using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LessonMvc.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;
using System.Web;

namespace LessonMvc.Controllers;

public class HelloController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HelloController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Merhaba(string categoryId, string category, int id)
    {
        return View();
        //return String.Format("categoryId = {0}, category = {1}, id = {2}",categoryId,category,id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
