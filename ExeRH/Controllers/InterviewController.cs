using ExeRH.Database;
using ExeRH.Models;
using ExeRH.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterviewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entity = new Interview();
                //entity.DislayName = viewModel.DisplayName;
                _database.Interviews.Add(entity);
                _database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _database.Interviews.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(ConvertToViewModel(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InterviewViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = _database.Interviews.Find(id);
                //entity.DislayName = viewModel.DisplayName;
                _database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
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
                .FirstOrDefault(i => i.Id.Equals(id));
            if (entity == null)
            {
                return NotFound();
            }
            return View(ConvertToViewModel(entity));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _database.Interviews.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            // TODO: Doesn’t delete the specified entity, it returns a view of the 
            // entity where you can submit (HttpPost) the deletion. Performing 
            // a delete operation in response to a GET request (or for that matter, 
            // performing an edit operation, create operation, or any other operation 
            // that changes data) opens up a security hole.
            //return View(professional);

            DeleteConfirmed(id.Value);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var entity = _database.Interviews.Find(id);
            _database.Interviews.Remove(entity);
            _database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
