using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Data;
using PortfolioWebsite.Models;
using PortfolioWebsite.ViewModel;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace PortfolioWebsite.Controllers
{
    public class AdminController : Controller
    {
        AppDbContext db;

        IWebHostEnvironment env;

        public AdminController(AppDbContext _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PersonalInfo()
        {
            var data = db.Tbl_PersonalInfo.FirstOrDefault();

            if (data != null)
            {
                PersonalInfoVM personalInfo = new PersonalInfoVM();
                personalInfo.Id = data.Id;
                personalInfo.Title = data.Title;
                personalInfo.SubTitle = data.SubTitle;
                ViewData["HomeImage"] = data.HomeImage;
                ViewData["AboutImage"] = data.AboutImage;
                personalInfo.Description = data.Description;
                personalInfo.CVLink = data.CVLink;
                personalInfo.Email = data.Email;
                personalInfo.Location = data.Location;
                personalInfo.LinkedInURL = data.LinkedInURL;
                personalInfo.LinkedInTitle = data.LinkedInTitle;
                personalInfo.InstagramURL = data.InstagramURL;
                personalInfo.InstagramTitle = data.InstagramTitle;
                personalInfo.GitHubURL = data.GitHubURL;
                personalInfo.GitHubTitle = data.GitHubTitle;

                return View(personalInfo);
            }
            return View();
        }

        [HttpPost]
        public IActionResult PersonalInfo(PersonalInfoVM myInfo, string? OldHomeImg, string? OldAboutImg)
        {
            string HomeImageName, AboutImageName;

            if (ModelState.IsValid)
            {
                var FolderPath = Path.Combine(env.WebRootPath, "images");

                if (myInfo.HomeImage != null)
                {
                    HomeImageName = myInfo.HomeImage.FileName.ToString();
                    var CompletePath = Path.Combine(FolderPath, HomeImageName);
                    myInfo.HomeImage.CopyTo(new FileStream(CompletePath, FileMode.Create));
                }
                else
                {
                    HomeImageName = OldHomeImg;
                }

                if (myInfo.AboutImage != null)
                {
                    AboutImageName = myInfo.AboutImage.FileName;
                    var FullPath = Path.Combine(FolderPath, AboutImageName);
                    myInfo.AboutImage.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                else
                {
                    AboutImageName = OldAboutImg;
                }

                PersonalInfo personalInfo = new PersonalInfo();
                personalInfo.Id = myInfo.Id;
                personalInfo.Title = myInfo.Title;
                personalInfo.SubTitle = myInfo.SubTitle;
                personalInfo.HomeImage = HomeImageName;
                personalInfo.AboutImage = AboutImageName;
                personalInfo.Description = myInfo.Description;
                personalInfo.CVLink = myInfo.CVLink;
                personalInfo.Email = myInfo.Email;
                personalInfo.Location = myInfo.Location;
                personalInfo.LinkedInURL = myInfo.LinkedInURL;
                personalInfo.LinkedInTitle = myInfo.LinkedInTitle;
                personalInfo.InstagramURL = myInfo.InstagramURL;
                personalInfo.InstagramTitle = myInfo.InstagramTitle;
                personalInfo.GitHubURL = myInfo.GitHubURL;
                personalInfo.GitHubTitle = myInfo.GitHubTitle;

                var data = db.Tbl_PersonalInfo.AsNoTracking().FirstOrDefault();

                if (data != null)
                {
                    db.Tbl_PersonalInfo.Update(personalInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.Tbl_PersonalInfo.Add(personalInfo);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Admin");
            }

            return View();
        }
    }
}
