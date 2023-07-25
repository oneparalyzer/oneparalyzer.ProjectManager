using Microsoft.AspNetCore.Mvc;

namespace oneparalyzer.ProjectManager.Api.Controllers;

public class EmployeesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}