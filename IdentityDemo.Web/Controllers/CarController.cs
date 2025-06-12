using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using IdentityDemo.Web.Views.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Web.Controllers
{
    [Route("car")]
    public class CarController(ICarService carService) : Controller
    {
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id) {
            var car = await carService.GetByIdAsync(id);

            var viewModel = new DetailsVM {
                Make = car!.Make,
                Model = car.Model,
                Year = car.Year
            };

            return View(viewModel);
        }

        [HttpGet("details")]
        public async Task<IActionResult> AllDetails() {
            var cars = await carService.GetAllAsync();

            var viewModel = new AllDetailsVM() {
                CarVMs = [.. cars
                    .Select(c => new AllDetailsVM.CarVM()
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year
                    })]
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                await carService.DeleteAsync(id);
            } catch(ArgumentException ex) {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(AllDetails));
        }

        private int CurrentYear => DateTime.Now.Year;
        private IEnumerable<int> GetYearRange => Enumerable.Range(1920, CurrentYear - 1920 + 2).Reverse();


        [HttpGet("create")]
        public IActionResult Create() {
            var viewModel = new CreateVM {
                Make = string.Empty, // Initialize required property
                Model = string.Empty, // Initialize required property
                Year = CurrentYear, // Initialize required property
                YearOptions = GetYearRange
            };
            return View(viewModel);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateVM viewModel) {
            viewModel.YearOptions = GetYearRange;

            if(!ModelState.IsValid)
                return View(viewModel);

            var model = new Car {
                Make = viewModel.Make,
                Model = viewModel.Model,
                Year = viewModel.Year
            };

            await carService.AddAsync(model);

            return RedirectToAction("AllDetails");
        }
    }
}