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
using System.Numerics;

namespace LessonMvc.Controllers;

public class BooksController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public BooksController(ILogger<HomeController> logger, IConfiguration configuration)
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

        using (DataContext dataContex = new DataContext(_configuration))
        {
            ViewData["books"] = dataContex.Book.ToList();

            
        };

        return View();

    }


    public IActionResult ListeModel()
    {
        IEnumerable<Book> liste = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            liste = dataContex.Book.ToList();
        };

        return View(liste);

    }

    public IActionResult Create()
    {

        List<String> tur = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            tur = dataContex.Category.Select(c => c.Name).ToList();
        }

        SelectList turler = new SelectList(tur);

        ViewData["tur"] = turler;

        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        int vSonuc = 0;
        List<String> tur = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            dataContex.Add(book);
            vSonuc = dataContex.SaveChanges();

            if (vSonuc > 0){
                ModelState.Clear(); 
                ViewData["Message"] = "Ekleme İşlemi Başarılı!";
            }
            else          
                ViewData["Message"] = "Hata Oluştu!";

            tur = dataContex.Category.Select(c => c.Name).ToList();

            //return RedirectToAction("Create",ViewBag);

        }


        SelectList turler = new SelectList(tur);

        ViewData["tur"] = turler;

        return View();
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
    public IActionResult Edit(Book book)
    {

        IEnumerable<Book> liste = null;

        using (DataContext dataContex = new DataContext(_configuration))
        {

            dataContex.Update(book);
            dataContex.SaveChanges();

            //liste = dataContex.Book.ToList();

        }

        //ViewBag.Baslik = Baslik;
        //ViewBag.Baslik = Aciklama;

        return RedirectToAction("listeModel");
    }

    public IActionResult Delete(int Id)
    {
        Book book = null;
        using(DataContext dataContext = new DataContext(_configuration) ){

            book = dataContext.Book.Where(b=>b.Id == Id).FirstOrDefault();

        }

        return View(book);

    }

    [HttpPost]
    public IActionResult Delete(Book book)
    {

        IEnumerable<Book> liste = null;

        using(DataContext dataContext = new DataContext(_configuration) ){


            if(book != null)
            {

                dataContext.Book.Remove(book);
                dataContext.SaveChanges();

                //liste = dataContext.Book.ToList();

                return RedirectToAction("ListeModel");

            }
        }

        return View();

    }

    public IActionResult Details(int Id){

        Book book = null;
        using(DataContext dataContext = new DataContext(_configuration))
        {
            book = dataContext.Book.Where(b=>b.Id==Id).FirstOrDefault();
        }
        return View(book);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
