﻿using CarTransports.Interfaces;
using CarTransports.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarTransports.Controllers
{
    public class DebitStateController : Controller
    {
        // Variables
        private readonly IDebitStateRepository repository;
        private readonly string Header = "Σταδια τιμολογησης";

        // Constructor
        public DebitStateController(IDebitStateRepository repository)
        {
            this.repository = repository;
        }

        // GET: DebitState/Index
        public IActionResult Index()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Ευρετηριο";

            return View("_TableIndex", repository.GetAll().OrderBy(o => o.DebitStateId).ToList());
        }

        // GET: DebitState/Create
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table");
        }

        // POST: DebitState/Create
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description")] DebitState item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Create(item);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes!");
            }

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table", item);
        }

        // GET: DebitState/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            var item = repository.GetById(id);

            return View("_Table", item);
        }

        // POST: DebitState/Edit/5
        [ActionName("Edit")]
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var item = repository.GetById(id);

            if (await TryUpdateModelAsync(item, "", s => s.Description))
            {
                try
                {
                    repository.Update(item);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            return View("_Table", item);
        }

        // GET: DebitState/Delete/5
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Διαγραφη";
            ViewBag.Action = "Delete";

            var item = repository.GetById(id);

            return View("_Table", item);
        }

        // POST: DebitState/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                var item = repository.GetById(id);

                repository.Delete(item);

                return RedirectToAction(nameof(Index));
            }

            return View("_Table");
        }
    }
}
