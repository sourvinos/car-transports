using CarTransports.Interfaces;
using CarTransports.Models;
using CarTransports.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarTransports.Controllers
{

    public class CustomerController : Controller
    {
        // Variables
        private readonly ICustomerRepository customerRepository;
        private readonly IPickupRepository pickupRepository;
        private readonly string Header = "Πελατες";

        // Constructor
        public CustomerController(ICustomerRepository customerRepository, IPickupRepository pickupRepository)
        {
            this.customerRepository = customerRepository;
            this.pickupRepository = pickupRepository;
        }

        // GET: Customer/Index
        public IActionResult Index()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Ευρετηριο";

            var customers = customerRepository.GetAll();
            var resultsCustomers = customers.Select(group => new CustomerGroupViewModel
            {
                CustomerId = group.CustomerId,
                CustomerDescription = group.GreekCompanyName,
                PickupPerCustomerCount = pickupRepository.GetPickups("", "").Count(a => a.CustomerId == group.CustomerId),
                SumPerCustomer = pickupRepository.GetPickups("", "").Where(a => a.CustomerId == group.CustomerId).Sum(a => a.Price),
                PickupResults = pickupRepository.GetPickups("", "").Where(a => a.CustomerId == group.CustomerId)
            });

            return View("_TableIndex", resultsCustomers.OrderBy(i => i.CustomerDescription));
        }

        // GET: Customer/Create
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table");
        }

        // POST: Customer/Create
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EnglishCompanyName, GreekCompanyName, ContactPerson, Address, City, Zip, CustomsClearer, Email, Phone, PhoneSecondary, Profession, Salutation, TaxNo, GenitiveCase")] Customer item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customerRepository.Create(item);

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

        // GET: Customer/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            var item = customerRepository.GetById(id);

            return View("_Table", item);
        }

        // POST: Customer/Edit/5
        [ActionName("Edit")]
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPostAsync(int id)
        {
            var item = customerRepository.GetById(id);

            if (await TryUpdateModelAsync(item, "", s => s.EnglishCompanyName, s => s.GreekCompanyName, s => s.ContactPerson, s => s.Address, s => s.City, s => s.Zip, s => s.CustomsClearer, s => s.Email, s => s.Phone, s => s.PhoneSecondary, s => s.Profession, s => s.Salutation, s => s.TaxNo, s => s.GenitiveCase))
            {
                try
                {
                    customerRepository.Update(item);

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

        // GET: Customer/Delete/5
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Διαγραφη";
            ViewBag.Action = "Delete";

            var item = customerRepository.GetById(id);

            return View("_Table", item);
        }

        // POST: Customer/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                var item = customerRepository.GetById(id);

                customerRepository.Delete(item);

                return RedirectToAction(nameof(Index));
            }

            return View("_Table");
        }
    }
}
