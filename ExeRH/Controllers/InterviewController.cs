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
            var entity = new ApplicantUser();
            PopulateAssignedSkillData(entity);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Interview entity, string[] selectedSkills)
        {
            var assigmentList = new List<UserSkillAssignment>();

            if (selectedSkills != null)
            {
                foreach (var s in selectedSkills)
                {
                    var skillAssignmentToAdd = new UserSkillAssignment
                    {
                        UserId = entity.UserId,
                        SkillId = int.Parse(s)
                    };
                    assigmentList.Add(skillAssignmentToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                entity.Date = DateTime.Now;
                _database.Interviews.Add(entity);
                _database.UserSkillAssignments.AddRange(assigmentList);
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
                .Include(i => i.User).ThenInclude(i => i.SkillAssignments)
                .Include(i => i.JobPosition).ThenInclude(i => i.JobPositionSkillsAssignments)
                .Where(i => i.Id == id)
                .Select(i => new InterviewDetailsViewModel()
                {
                    Date = i.Date,
                    JobPositionName = i.JobPosition.DisplayName,
                    UserName = i.User.FullName,
                    Skills = i.User.SkillAssignments
                            .Select(j => new AssignedSkillData
                            {
                                SkillName = j.Skill.DislayName
                            }).ToList()
                })
                .SingleOrDefault();
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
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

        private void PopulateAssignedSkillData(ApplicantUser user)
        {
            var jobSkills = new HashSet<int>();

            var allSkills = _database.Skills;
            if (user != null)
            {
                Parallel.ForEach(
                   user.SkillAssignments.Select(js => js.SkillId),
                   i => jobSkills.Add(i));
            }

            var viewModel = new List<AssignedSkillData>();
            foreach (var s in allSkills)
            {
                viewModel.Add(new AssignedSkillData
                {
                    SkillId = s.Id,
                    SkillName = s.DislayName,
                    Assigned = jobSkills.Contains(s.Id),
                });
            }

            ViewData["AssignedSkills"] = viewModel
                .OrderByDescending(i => i.Assigned)
                .ThenBy(i => i.SkillName);
        }
    }
}
