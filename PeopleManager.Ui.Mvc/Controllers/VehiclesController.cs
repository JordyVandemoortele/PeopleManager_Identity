using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly PersonService _personService;

        public VehiclesController(VehicleService vehicleService, PersonService personService)
        {
            _vehicleService = vehicleService;
            _personService = personService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _vehicleService.Find();

            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Create", vehicle);
            }

            _vehicleService.Create(vehicle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vehicle = _vehicleService.Get(id);

            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView("Edit", vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [FromForm]Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Edit", vehicle);
            }

            _vehicleService.Update(id, vehicle);

            return RedirectToAction("Index");
        }
        
        private IActionResult CreateEditView([AspMvcView]string viewName, Vehicle? vehicle = null)
        {
            var people = _personService.Find();

            ViewBag.People = people;

            return View(viewName, vehicle);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vehicle = _vehicleService.Get(id);

            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpPost("Vehicles/Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _vehicleService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
