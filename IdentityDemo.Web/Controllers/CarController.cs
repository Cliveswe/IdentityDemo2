using IdentityDemo.Application.Cars;
using IdentityDemo.Domain.Entities;
using IdentityDemo.Web.Views.Account;
using IdentityDemo.Web.Views.Car;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IdentityDemo.Web.Controllers
{
    [Route("car")]
    public class CarController(ICarService carService) : Controller
    {
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var car = await carService.GetByIdAsync(id);

            var viewModel = new DetailsVM
            {
                Make = car!.Make,
                Model = car.Model,
                Year = car.Year
            };

            return View(viewModel);
        }

        [HttpGet("details")]
        public async Task<IActionResult> AllDetails()
        {
            var cars = await carService.GetAllAsync();

            var viewModel = new AllDetailsVM()
            {
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

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await carService.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(AllDetails));
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            //throw new Exception("test");
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var model = new Car
            {
                Make = viewModel.Make,
                Model = viewModel.Model,
                Year = viewModel.Year
            };

            await carService.AddAsync(model);

            return RedirectToAction("AllDetails");
        }
    }
}