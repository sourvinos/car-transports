using AutoMapper;
using CarTransports.Interfaces;
using CarTransports.Models;
using CarTransports.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarTransports.Controllers
{

    public class PickupPointController : Controller
    {
        // Variables
        private readonly ICountryRepository countryRepository;
        private readonly IPickupPointRepository pickupPointRepository;
        private readonly IMapper mapper;
        private readonly string Header = "Σημεια παραλαβης";

        // Constructor
        public PickupPointController(ICountryRepository countryRepository, IPickupPointRepository pickupPointRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.pickupPointRepository = pickupPointRepository;
            this.mapper = mapper;
        }

        // GET: PickupPoint/Index
        public IActionResult Index(string countryId)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Ευρετηριο";

            // Χωρα
            var countries = countryRepository.GetAll().OrderBy(o => o.Description);
            var resultsCountries = countries.Select(group => new CountryGroupViewModel
            {
                CountryId = group.CountryId,
                CountryDescription = group.Description,
                CountryCount = pickupPointRepository.GetAllWithCountries().Count(a => a.CountryId == group.CountryId),
            });

            // Populate View Model
            PickupPointIndexViewModel vm = new PickupPointIndexViewModel
            {
                CountryGroupViewModel = resultsCountries,
                PickupPointResults = pickupPointRepository.GetPickupPoints(countryId)
            };

            return View(vm);
        }

        // GET: PickupPoint/Create
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            PickupPointViewModel vm = new PickupPointViewModel
            {
                Countries = pickupPointRepository.PopulateDropDown("CountryId", "Description", "countries", "Description"),
            };

            return View("_Table", vm);
        }

        // POST: PickupPoint/Create
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PickupPointViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PickupPoint pickupPoint = mapper.Map<PickupPoint>(vm);
                    pickupPointRepository.Create(pickupPoint);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes!");
            }

            vm.Countries = pickupPointRepository.PopulateDropDown("CountryId", "Description", "countries", "Description");

            ViewBag.Header = Header;
            ViewBag.SubHeader = "Δημιουργια";
            ViewBag.Action = "Create";

            return View("_Table", vm);
        }

        // GET: PickupPoint/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Μεταβολη";
            ViewBag.Action = "Edit";

            var item = pickupPointRepository.GetById(id);

            PickupPointViewModel vm = new PickupPointViewModel
            {
                PickupPointId = item.PickupPointId,
                CountryId = item.CountryId,
                Description = item.Description,
                Zip = item.Zip,
                City = item.City,
                Address = item.Address,
                WorkingHours = item.WorkingHours,
                Lat = item.Lat,
                Lng = item.Lng,

                Countries = pickupPointRepository.PopulateDropDown("CountryId", "Description", "countries", "Description"),
            };

            return View("_Table", vm);
        }

        // POST: PickupPoint/Edit/5
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PickupPointViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pickupPoint = mapper.Map<PickupPoint>(vm);
                    pickupPointRepository.Update(pickupPoint);

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

            vm.Countries = pickupPointRepository.PopulateDropDown("CountryId", "Description", "countries", "Description");

            return View("_Table", vm);
        }

        // GET: PickupPoint/Delete/5
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            ViewBag.Header = Header;
            ViewBag.SubHeader = "Διαγραφη";
            ViewBag.Action = "Delete";

            var item = pickupPointRepository.GetById(id);

            PickupPointViewModel vm = new PickupPointViewModel
            {
                Countries = pickupPointRepository.PopulateDropDown("CountryId", "Description", "countries", "Description"),
            };

            return View("_Table", vm);
        }

        // POST: PickupPoint/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                var item = pickupPointRepository.GetById(id);

                pickupPointRepository.Delete(item);

                return RedirectToAction(nameof(Index));
            }

            return View("_Table");
        }
    }
}
