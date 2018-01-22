using ExeRH.Database;
using ExeRH.Models;
using ExeRH.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Controllers
{
    public class InterviewController : Controller
    {
        private readonly DatabaseContext _database;

        public InterviewController(DatabaseContext database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModels = _database.Interviews
                .Include(i => i.User)
                .Include(i => i.JobPosition)
                .OrderByDescending(i => i.Date)
                .Select(s => ConvertToViewModel(s))
                .ToList();
            return View(viewModels);
        }

        private static InterviewViewModel ConvertToViewModel(Interview entity)
        {
            return new InterviewViewModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                User = UserController.ConvertToViewModel(entity.User),
                JobPosition = JobController.ConvertToViewModel(entity.JobPosition)
                //Skills
            };
        }

        public IActionResult Create()
        {
            PopulateUsersDropDownList();
            PopulateJobPositionsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Interview entity)
        {
            if (ModelState.IsValid)
            {
                entity.Date = DateTime.Now;
                _database.Interviews.Add(entity);
                _database.SaveChanges();
                return RedirectToAction("Index");
            }

            // Error
            PopulateUsersDropDownList(entity.UserId);
            PopulateJobPositionsDropDownList(entity.JobPositionId);
            return View(entity);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _database.Interviews
                .Include(i => i.User)
                .Include(i => i.JobPosition)
                .AsNoTracking()
                .SingleOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(ConvertToViewModel(entity));
        }

        private void PopulateUsersDropDownList(object selectedValue = null)
        {
            var users = _database.Users
                .OrderBy(u => u.FullName)
                .Select(u => UserController.ConvertToViewModel(u));

            ViewBag.AllUsers = new SelectList(
                users.AsNoTracking(),
                nameof(ApplicantUserViewModel.Id),
                nameof(ApplicantUserViewModel.FullName),
                selectedValue);
        }

        private void PopulateJobPositionsDropDownList(object selectedValue = null)
        {
            var jobs = _database.JobPositions
                .OrderByDescending(u => u.Id)
                .Select(u => JobController.ConvertToViewModel(u));

            ViewBag.AllJobsPositions = new SelectList(
                jobs.AsNoTracking(),
                nameof(JobPositionViewModel.Id),
                nameof(JobPositionViewModel.DisplayName),
                selectedValue);
        }
    }
}
