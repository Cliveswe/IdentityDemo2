using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Web.Controllers;
public class ErrorController : Controller
{
    [HttpGet("error/exception")]
    public IActionResult ServerError() {
        return View();
    }

    [HttpGet("error/http/{statusCode}")]
    public IActionResult HttpError(int statusCode) {
        return View(statusCode);
    }



    //[HttpGet("error/{statusCode:int}")]
    //public IActionResult Error(int statusCode) {
    //    return View("Error", statusCode);
    //}

}
