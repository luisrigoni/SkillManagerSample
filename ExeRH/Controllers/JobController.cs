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
    public class JobController : Controller
    {
        private readonly DatabaseContext _database;

        public JobController(DatabaseContext database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModels = _database.JobPositions
                .Include(i => i.JobPositionSkillsAssignments).ThenInclude(js => js.Skill)
                .AsNoTracking()
                .ToList();
            return View(viewModels);
        }

        public static JobPositionViewModel ConvertToViewModel(JobPosition entity)
        {
            return new JobPositionViewModel()
            {
                Id = entity.Id,
                DisplayName = entity.DisplayName,
                //Skills = entity.JobPositionSkills?.Select(js => SkillController.ConvertToViewModel()).ToList()
            };
        }

        public IActionResult Create()
        {
            var entity = new JobPosition();
            entity.JobPositionSkillsAssignments = new List<JobPositionSkillAssignment>();
            PopulateAssignedSkillData(entity);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string displayName, string[] selectedSkills)
        {
            JobPosition model = new JobPosition();
            model.DisplayName = displayName;

            if (selectedSkills != null)
            {
                model.JobPositionSkillsAssignments = new List<JobPositionSkillAssignment>();
                foreach (var s in selectedSkills)
                {
                    var courseToAdd = new JobPositionSkillAssignment
                    {
                        JobPositionId = model.Id,
                        SkillId = int.Parse(s)
                    };
                    model.JobPositionSkillsAssignments.Add(courseToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                _database.JobPositions.Add(model);
                _database.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedSkillData(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _database.JobPositions
                .Include(i => i.JobPositionSkillsAssignments)
                .SingleOrDefault(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            PopulateAssignedSkillData(entity);
            return View(entity);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int? id, string[] selectedSkills)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entityToUpdate = await _database.JobPositions
                .Include(i => i.JobPositionSkillsAssignments).ThenInclude(i => i.Skill)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (await TryUpdateModelAsync(entityToUpdate, "", i => i.DisplayName))
            {
                UpdateJobPositionSkillsAssignments(selectedSkills, entityToUpdate);
                await _database.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            UpdateJobPositionSkillsAssignments(selectedSkills, entityToUpdate);
            PopulateAssignedSkillData(entityToUpdate);
            return View(entityToUpdate);
        }

        private void UpdateJobPositionSkillsAssignments(string[] selectedSkills, JobPosition entityToUpdate)
        {
            if (selectedSkills == null)
            {
                entityToUpdate.JobPositionSkillsAssignments = new List<JobPositionSkillAssignment>();
                return;
            }

            var selectedHS = new HashSet<string>(selectedSkills);
            var currentSkills = new HashSet<int>
                (entityToUpdate.JobPositionSkillsAssignments.Select(c => c.SkillId));
            foreach (var s in _database.Skills)
            {
                if (selectedHS.Contains(s.Id.ToString()))
                {
                    if (!currentSkills.Contains(s.Id))
                    {
                        entityToUpdate.JobPositionSkillsAssignments
                            .Add(new JobPositionSkillAssignment
                            {
                                JobPositionId = entityToUpdate.Id,
                                SkillId = s.Id
                            });
                    }
                }
                else
                {
                    if (currentSkills.Contains(s.Id))
                    {
                        var entityToRemove = entityToUpdate.JobPositionSkillsAssignments
                            .SingleOrDefault(i => i.SkillId == s.Id);
                        _database.Remove(entityToRemove);
                    }
                }
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _database.JobPositions.Find(id);
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
            var entity = _database.JobPositions.Find(id);
            _database.JobPositions.Remove(entity);
            _database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Report(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _database.JobPositions.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(ConvertToViewModel(entity));
        }

        private void PopulateAssignedSkillData(JobPosition jobPosition)
        {
            var jobSkills = new HashSet<int>();

            var allSkills = _database.Skills;
            if (jobPosition != null)
            {
                Parallel.ForEach(
                   jobPosition.JobPositionSkillsAssignments.Select(js => js.SkillId),
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
