using AutoMapper;
using CarTransports.Implementations;
using CarTransports.Interfaces;
using CarTransports.Models;
using CarTransports.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.Linq;

namespace CarTransports.Controllers
{

    public class PickupController : Controller
    {
        // Variables
        private readonly IPickupStateRepository pickupStateRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IDebitStateRepository debitStateRepository;
        private readonly ICollaboratorRepository collaboratorRepository;
        private readonly IPickupRepository pickupRepository;
        private readonly IMapper mapper;
        private readonly string Header = "Παραλαβες";

        // Constructor
        public PickupController(IPickupStateRepository pickupStateRepository, ICustomerRepository customerRepository, IDebitStateRepository debitStateRepository, ICollaboratorRepository collaboratorRepository, IPickupRepository pickupRepository, IMapper mapper)
        {
            this.pickupStateRepository = pickupStateRepository;
            this.customerRepository = customerRepository;
            this.debitStateRepository = debitStateRepository;
            this.collaboratorRepository = collaboratorRepository;
            this.pickupRepository = pickupRepository;
            this.mapper = mapper;
        }

        // GET: Pickup/Index
        public IActionResult Index(string pickupStateId, string debitStateId)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Ευρετηριο";

            // Σταδιο παραλαβης
            var pickupStates = pickupStateRepository.GetAll();
            var resultsPickupStates = pickupStates.Select(group => new PickupStateGroupViewModel
            {
                PickupStateId = group.PickupStateId,
                PickupStateDescription = group.Description,
                PickupPerStateCount = pickupRepository.GetPickups("", "").Count(a => a.PickupStateId == group.PickupStateId),
            });

            // Σταδιο τιμολογησης
            var debitStates = debitStateRepository.GetAll();
            var resultsDebitStates = debitStates.Select(group => new DebitStateGroupViewModel
            {
                DebitStateId = group.DebitStateId,
                DebitStateDescription = group.Description,
                DebitPerStateCount = pickupRepository.GetPickups("", "").Count(a => a.DebitStateId == group.DebitStateId),
                DebitAmount = pickupRepository.GetPickups("", "").Where(a => a.DebitStateId == group.DebitStateId).Sum(a => a.Price)
            });

            // Populate View Model
            PickupIndexViewModel vm = new PickupIndexViewModel
            {
                PickupStateGroupViewModel = resultsPickupStates,
                DebitStateGroupViewModel = resultsDebitStates,
                PickupResults = pickupRepository.GetPickups(pickupStateId, debitStateId)
            };

            return View(vm);
        }

        // GET: Pickup/Create
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            PickupViewModel vm = new PickupViewModel
            {
                Customers = pickupRepository.PopulateDropDown("CustomerId", "GreekCompanyName", "customers", "GreekCompanyName"),
                PickupPoints = pickupRepository.PopulateDropDown("PickupPointId", "Description", "PickupPoints", "Description"),
                PickupStates = pickupRepository.PopulateDropDown("PickupStateId", "Description", "PickupStates", "PickupStateId"),
                DestinationPorts = pickupRepository.PopulateDropDown("PortId", "Description", "Ports", "Description"),
                DebitStates = pickupRepository.PopulateDropDown("DebitStateId", "Description", "DebitStates", "DebitStateId"),
                Suppliers = pickupRepository.PopulateDropDown("SupplierId", "Description", "Suppliers", "Description"),
                CurrentPositions = pickupRepository.PopulateDropDown("CurrentPositionId", "Description", "CurrentPositions", "Description")
            };

            return View("_Table", vm);
        }

        // POST: Pickup/Create
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PickupViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pickup pickup = mapper.Map<Pickup>(vm);
                    pickupRepository.Create(pickup);

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

            vm.Customers = pickupRepository.PopulateDropDown("CustomerId", "GreekCompanyName", "customers", "GreekCompanyName");
            vm.PickupPoints = pickupRepository.PopulateDropDown("PickupPointId", "Description", "PickupPoints", "Description");
            vm.PickupStates = pickupRepository.PopulateDropDown("PickupStateId", "Description", "PickupStates", "PickupStateId");
            vm.DestinationPorts = pickupRepository.PopulateDropDown("PortId", "Description", "Ports", "Description");
            vm.DebitStates = pickupRepository.PopulateDropDown("DebitStateId", "Description", "DebitStates", "DebitStateId");
            vm.Suppliers = pickupRepository.PopulateDropDown("SupplierId", "Description", "Suppliers", "Description");
            vm.CurrentPositions = pickupRepository.PopulateDropDown("CurrentPositionId", "Description", "CurrentPositions", "Description");

            return View("_Table", vm);
        }

        // GET: Pickup/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            var item = pickupRepository.GetById(id);

            PickupViewModel vm = new PickupViewModel
            {
                PickupId = item.PickupId,
                CustomerId = item.CustomerId,
                PickupPointId = item.PickupPointId,
                PickupStateId = item.PickupStateId,
                DestinationPortId = item.DestinationPortId,
                DebitStateId = item.DebitStateId,
                SupplierId = item.SupplierId,
                CurrentPositionId = item.CurrentPositionId,
                CarNo = item.CarNo,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                PickupNo = item.PickupNo,
                Price = item.Price,
                VIN = item.VIN,
                Customers = pickupRepository.PopulateDropDown("CustomerId", "GreekCompanyName", "customers", "GreekCompanyName"),
                PickupPoints = pickupRepository.PopulateDropDown("PickupPointId", "Description", "PickupPoints", "Description"),
                PickupStates = pickupRepository.PopulateDropDown("PickupStateId", "Description", "PickupStates", "PickupStateId"),
                DestinationPorts = pickupRepository.PopulateDropDown("PortId", "Description", "Ports", "Description"),
                DebitStates = pickupRepository.PopulateDropDown("DebitStateId", "Description", "DebitStates", "DebitStateId"),
                Suppliers = pickupRepository.PopulateDropDown("SupplierId", "Description", "Suppliers", "Description"),
                CurrentPositions = pickupRepository.PopulateDropDown("CurrentPositionId", "Description", "CurrentPositions", "Description")
            };

            return View("_Table", vm);
        }

        // POST: Pickup/Edit/5
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PickupViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pickup = mapper.Map<Pickup>(vm);
                    pickupRepository.Update(pickup);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes!");
            }

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            vm.Customers = pickupRepository.PopulateDropDown("CustomerId", "GreekCompanyName", "customers", "GreekCompanyName");
            vm.PickupPoints = pickupRepository.PopulateDropDown("PickupPointId", "Description", "PickupPoints", "Description");
            vm.PickupStates = pickupRepository.PopulateDropDown("PickupStateId", "Description", "PickupStates", "PickupStateId");
            vm.DestinationPorts = pickupRepository.PopulateDropDown("PortId", "Description", "Ports", "Description");
            vm.DebitStates = pickupRepository.PopulateDropDown("DebitStateId", "Description", "DebitStates", "DebitStateId");
            vm.Suppliers = pickupRepository.PopulateDropDown("SupplierId", "Description", "Suppliers", "Description");
            vm.CurrentPositions = pickupRepository.PopulateDropDown("CurrentPositionId", "Description", "CurrentPositions", "Description");

            return View("_Table", vm);
        }

        // GET: Pickup/Delete/5
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Διαγραφη";
            ViewBag.Action = "Delete";

            var item = pickupRepository.GetById(id);

            PickupViewModel vm = new PickupViewModel
            {
                PickupId = item.PickupId,
                CustomerId = item.CustomerId,
                PickupPointId = item.PickupPointId,
                PickupStateId = item.PickupStateId,
                DestinationPortId = item.DestinationPortId,
                DebitStateId = item.DebitStateId,
                SupplierId = item.SupplierId,
                CurrentPositionId = item.CurrentPositionId,
                CarNo = item.CarNo,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                PickupNo = item.PickupNo,
                Price = item.Price,
                VIN = item.VIN,
                Customers = pickupRepository.PopulateDropDown("CustomerId", "GreekCompanyName", "customers", "GreekCompanyName"),
                PickupPoints = pickupRepository.PopulateDropDown("PickupPointId", "Description", "PickupPoints", "Description"),
                PickupStates = pickupRepository.PopulateDropDown("PickupStateId", "Description", "PickupStates", "PickupStateId"),
                DestinationPorts = pickupRepository.PopulateDropDown("PortId", "Description", "Ports", "Description"),
                DebitStates = pickupRepository.PopulateDropDown("DebitStateId", "Description", "DebitStates", "DebitStateId"),
                Suppliers = pickupRepository.PopulateDropDown("SupplierId", "Description", "Suppliers", "Description"),
                CurrentPositions = pickupRepository.PopulateDropDown("CurrentPositionId", "Description", "CurrentPositions", "Description")
            };

            return View("_Table", vm);
        }

        // POST: Pickup/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                var item = pickupRepository.GetById(id);

                pickupRepository.Delete(item);

                return RedirectToAction(nameof(Index));
            }

            return View("_Table");
        }

        // AJAX: Pickup/Results
        public ActionResult Results(PickupIndexViewModel vm)
        {
            vm.PickupResults = pickupRepository.GetPickups(vm.PickupStateId, vm.DebitStateId);

            return PartialView("_results", vm);
        }

        // Helper: PopulateCollaborators
        private void PopulateCollaborators(PickupViewModel vm)
        {
            vm.Collaborators = collaboratorRepository.GetAllWithExpenses().ToList();
        }

        // Invoices https://localhost:44337/pickup/invoicesForCustomer?customerId=23&pickupIds=169
        public IActionResult InvoicesForCustomer(string customerIds, string pickupIds)
        {
            var vm = new InvoicesForCustomerViewModel
            {
                Customer = customerRepository.GetById(Int32.Parse(customerIds))
            };

            foreach (var pickupId in pickupIds.Split(','))
            {
                vm.Pickups.Add(pickupRepository.GetById(Int32.Parse(pickupId)));
            }

            return new ViewAsPdf("_InvoicesForCustomer", vm);

            //return new ViewAsPdf("_InvoicesForCustomer", vm)
            //{
            //    FileName = "Report.pdf",
            //    PageSize = Size.A4,
            //    PageOrientation = Orientation.Landscape,
            //    PageMargins = { Left = 0, Right = 0 }
            //};

            //return PartialView("_InvoicesForCustomer", vm);
        }
    }
}
