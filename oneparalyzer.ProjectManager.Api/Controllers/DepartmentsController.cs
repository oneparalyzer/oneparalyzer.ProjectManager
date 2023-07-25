using Microsoft.AspNetCore.Mvc;

namespace oneparalyzer.ProjectManager.Api.Controllers;

public class DepartmentsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}