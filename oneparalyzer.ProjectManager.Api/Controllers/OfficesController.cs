using Microsoft.AspNetCore.Mvc;

namespace oneparalyzer.ProjectManager.Api.Controllers;

public class OfficesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}