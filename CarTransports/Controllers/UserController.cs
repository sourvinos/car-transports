using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTransports.Identity;
using CarTransports.ViewModels;

namespace CarTransports.Controllers
{
    [Authorize(Roles = "Admins")]
    public class UserController : Controller
    {
        // Variables
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly string Header = "Χρηστες";

        // Constructor
        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: User/Index
        public IActionResult Index()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Ευρετηριο";

            List<UserIndexViewModel> vm = new List<UserIndexViewModel>();

            vm = userManager.Users.Select(u => new UserIndexViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();

            return View("_TableIndex", vm);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            UserViewModel vm = new UserViewModel
            {
                AppRoles = PopulateRolesDropDown()
            };

            return View("_Table", vm);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Name = vm.Name,
                    UserName = vm.UserName,
                    Email = vm.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    AppRole appRole = await roleManager.FindByIdAsync(vm.AppRoleId);

                    if (appRole != null)
                    {
                        IdentityResult roleResult = await userManager.AddToRoleAsync(user, appRole.Name);

                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            vm = new UserViewModel
            {
                AppRoles = PopulateRolesDropDown()
            };

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table", vm);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            UserViewModel vm = new UserViewModel
            {
                AppRoles = PopulateRolesDropDown()
            };

            if (!String.IsNullOrEmpty(id))
            {
                AppUser user = await userManager.FindByIdAsync(id);

                if (user != null)
                {
                    vm.Name = user.Name;
                    vm.Email = user.Email;
                    vm.UserName = user.UserName;
                    vm.AppRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }

            return View("_Table", vm);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(id);

                if (user != null)
                {
                    user.Name = vm.Name;
                    user.Email = vm.Email;
                    user.UserName = vm.UserName;

                    string existingRole = userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;

                    IdentityResult result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        if (existingRoleId != vm.AppRoleId)
                        {
                            IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRole);

                            if (roleResult.Succeeded)
                            {
                                AppRole applicationRole = await roleManager.FindByIdAsync(vm.AppRoleId);

                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);

                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }

                        return RedirectToAction("Index");
                    }
                }
            }

            vm = new UserViewModel
            {
                AppRoles = PopulateRolesDropDown()
            };

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            return View("_Table", vm);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Διαγραφη";
            ViewBag.Action = "Delete";

            UserViewModel vm = new UserViewModel
            {
                AppRoles = PopulateRolesDropDown()
            };

            if (!String.IsNullOrEmpty(id))
            {
                AppUser user = await userManager.FindByIdAsync(id);

                if (user != null)
                {
                    vm.Name = user.Name;
                    vm.Email = user.Email;
                    vm.UserName = user.UserName;
                    vm.AppRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }

            return View("_Table", vm);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                AppUser user = await userManager.FindByIdAsync(id);

                if (user != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View("_Table");
        }

        // Helper: Populate Roles DropDown
        private List<SelectListItem> PopulateRolesDropDown()
        {
            return roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
        }
    }
}
