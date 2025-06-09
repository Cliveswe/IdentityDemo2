using IdentityDemo.Application.Cars;
using IdentityDemo.Web.Views.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IdentityDemo.Web.Controllers
{
    [Route("car")]
    public class CarController(ICarService carService) : Controller
    {
        [HttpGet("details/{id}")]
        public IActionResult Details(int id, string name, int age)
        {
            //return Json(new { Name = "Lille Bo", Age = 5 });
            return Content($"I Details, Id: {id}");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            //throw new Exception("test");
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var model = new Car
            {
                CompanyName = viewModel.CompanyName,
                City = viewModel.City,
            };

            await carService.AddAsync(model);
            return View();
            //return RedirectToAction(nameof(Index));
        }
    }
}