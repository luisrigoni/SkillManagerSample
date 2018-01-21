using ExeRH.Database;
using ExeRH.Models;
using ExeRH.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
                .Select(s => ConvertToViewModel(s))
                .ToList();
            return View(viewModels);
        }

        private static JobPositionViewModel ConvertToViewModel(JobPosition entity)
        {
            return new JobPositionViewModel()
            {
                Id = entity.Id,
                DisplayName = entity.DisplayName,
            };
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobPositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entity = new JobPosition();
                entity.DisplayName = viewModel.DisplayName;
                _database.JobPositions.Add(entity);
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

            var entity = _database.JobPositions.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(ConvertToViewModel(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, JobPositionViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = _database.JobPositions.Find(id);
                entity.DisplayName = viewModel.DisplayName;
                _database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
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
    }
}
