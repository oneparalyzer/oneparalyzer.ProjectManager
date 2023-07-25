using Microsoft.AspNetCore.Mvc;

namespace oneparalyzer.ProjectManager.Api.Controllers;

public class PostsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}