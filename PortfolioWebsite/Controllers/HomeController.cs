using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db;

        public HomeController(AppDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            ViewBag.PersonalInfo = db.Tbl_PersonalInfo.FirstOrDefault();
            ViewBag.Services = db.Tbl_Service;
            ViewBag.Skills = db.Tbl_Skill;
            ViewBag.Projects = db.Tbl_Project.Include(x => x.Categories).GroupBy(x => x.Categories).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Contact.Add(contact);
                db.SaveChanges();

                ViewData["MessageStatus"] = "Message Send Successfully";
            }
            else
            {
                ViewData["MessageStatus"] = "Message Send Failed";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
