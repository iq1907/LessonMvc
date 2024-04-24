using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LessonMvc.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;
using System.Web;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LessonMvc.Helpers;

namespace LessonMvc.Controllers;

public class BooksController_ : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public BooksController_(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Liste()
    {
        ViewBag.UrunIsmi = "Mvc Dünyasına Hoşgeldiniz.";
        ViewBag.StokAdet = 5;

        List<Book> books = null;

        using (DataContext dataContext = new DataContext(_configuration))
        {
            books = dataContext.Book.ToList();
        }

        ViewData["books"] = books;

        return View();
    }

    public IActionResult Detay()
    {
        Book book = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {
            book = dataContex.Book.FirstOrDefault();
        }

        ViewData["book"] = book;

        return View();
    }

    public IActionResult ListeModel()
    {
        IEnumerable<Book> Liste = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            Liste = dataContex.Book.ToList();
        };

        return View(Liste);

    }

    public IActionResult Create()
    {

        Book book = null;

        List<String> tur = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            book = dataContex.Book.FirstOrDefault();

            tur = dataContex.Category.Select(c => c.Name).ToList();
        }

        SelectList turler = new SelectList(tur);

        ViewData["tur"] = turler;

        return View(book);
    }

    [HttpPost]
    public IActionResult Create(/*String Baslik, String Aciklama*/ Book book)
    {

        //ViewBag.Baslik = Baslik;
        //ViewBag.Baslik = Aciklama;

        return View("Thanks", book);
    }

    public IActionResult Edit(int Id)
    {

        Book book = null;

        List<String> tur = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            book = dataContex.Book.Where(b => b.Id == Id).FirstOrDefault();

            tur = dataContex.Category.Select(c => c.Name).ToList();
        }

        SelectList turler = new SelectList(tur);

        ViewData["tur"] = turler;

        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(/*String Baslik, String Aciklama*/ Book book)
    {

        IEnumerable<Book> liste = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            dataContex.Update(book);
            dataContex.SaveChanges();

            liste = dataContex.Book.ToList();

            //return RedirectToAction("Home","Index");
        }

        //ViewBag.Baslik = Baslik;
        //ViewBag.Baslik = Aciklama;

        return View("ListeModel", liste);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
