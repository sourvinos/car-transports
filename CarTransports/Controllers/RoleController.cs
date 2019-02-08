using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTransports.Identity;
using CarTransports.ViewModels;

namespace CarTransports.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RoleController : Controller
    {
        // Variables
        private readonly RoleManager<AppRole> roleManager;
        private string Header = "Ρολοι χρηστων";

        // Constructor
        public RoleController(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        // GET: Role/Index
        public IActionResult Index()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Ευρετηριο";

            List<RoleIndexViewModel> vm = new List<RoleIndexViewModel>();

            vm = roleManager.Roles.Select(r => new RoleIndexViewModel
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToList();

            return View("_TableIndex", vm);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table");
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, RoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);

                AppRole applicationRole = isExist ? await roleManager.FindByIdAsync(id) : new AppRole { };

                applicationRole.Name = vm.RoleName;

                IdentityResult roleRuslt = isExist ? await roleManager.UpdateAsync(applicationRole) : await roleManager.CreateAsync(applicationRole);

                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table", vm);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            RoleViewModel vm = new RoleViewModel();

            if (!String.IsNullOrEmpty(id))
            {
                AppRole applicationRole = await roleManager.FindByIdAsync(id);

                if (applicationRole != null)
                {
                    vm.Id = applicationRole.Id;
                    vm.RoleName = applicationRole.Name;
                }
            }

            return View("_Table", vm);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);

                AppRole applicationRole = isExist ? await roleManager.FindByIdAsync(id) : new AppRole { };

                applicationRole.Name = vm.RoleName;

                IdentityResult roleRuslt = isExist ? await roleManager.UpdateAsync(applicationRole) : await roleManager.CreateAsync(applicationRole);

                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            return View("_Table", vm);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Διαγραφη";
            ViewBag.Action = "Delete";

            RoleViewModel vm = new RoleViewModel();

            if (!String.IsNullOrEmpty(id))
            {
                AppRole applicationRole = await roleManager.FindByIdAsync(id);

                if (applicationRole != null)
                {
                    vm.Id = applicationRole.Id;
                    vm.RoleName = applicationRole.Name;
                }
            }

            return View("_Table", vm);
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            AppRole applicationRole = await roleManager.FindByIdAsync(id);

            if (applicationRole != null)
            {
                IdentityResult roleResult = roleManager.DeleteAsync(applicationRole).Result;

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("_Table");
        }
    }
}
