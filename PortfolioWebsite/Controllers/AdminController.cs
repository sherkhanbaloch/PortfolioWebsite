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
            ViewData["TotalCategories"] = db.Tbl_Category.Count();
            ViewData["TotalMessages"] = db.Tbl_Contact.Count();
            ViewData["TotalProjects"] = db.Tbl_Project.Count();
            ViewData["TotalServices"] = db.Tbl_Service.Count();

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

        public IActionResult AllServices()
        {
            var service = db.Tbl_Service;
            return View(service);
        }

        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddService(Service service)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Service.Add(service);
                db.SaveChanges();

                return RedirectToAction("AllServices", "Admin");
            }
            return View();
        }

        public IActionResult UpdateService(int id)
        {
            var data = db.Tbl_Service.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateService(Service service)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Service.Update(service);
                db.SaveChanges();

                return RedirectToAction("AllServices", "Admin");
            }
            return View();
        }

        public IActionResult DeleteService(int Id)
        {
            var data = db.Tbl_Service.Find(Id);

            if (data != null)
            {
                db.Tbl_Service.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("AllServices", "Admin");
        }

        public IActionResult AllSkills()
        {
            var skills = db.Tbl_Skill;
            return View(skills);
        }

        public IActionResult AddSkill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSkill(Skill skill)
        {
            if (ModelState.IsValid)
            {
                var order = db.Tbl_Skill.Max(x => (int?)x.DisplayOrder) ?? 0;
                skill.DisplayOrder = order + 1;

                db.Tbl_Skill.Add(skill);
                db.SaveChanges();

                return RedirectToAction("AllSkills", "Admin");
            }
            return View();
        }

        public IActionResult UpdateSkill(int id)
        {
            var data = db.Tbl_Skill.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Skill.Update(skill);
                db.SaveChanges();

                return RedirectToAction("AllSkills", "Admin");
            }
            return View();
        }

        public IActionResult DeleteSkill(int Id)
        {
            var data = db.Tbl_Skill.Find(Id);

            if (data != null)
            {
                db.Tbl_Skill.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("AllSkills", "Admin");
        }

        public IActionResult AllCategories()
        {
            var category = db.Tbl_Category;
            return View(category);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Category.Add(category);
                db.SaveChanges();

                return RedirectToAction("AllCategories", "Admin");
            }
            return View();
        }

        public IActionResult UpdateCategory(int id)
        {
            var data = db.Tbl_Category.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Category.Update(category);
                db.SaveChanges();

                return RedirectToAction("AllCategories", "Admin");
            }
            return View();
        }

        public IActionResult DeleteCategory(int Id)
        {
            var data = db.Tbl_Category.Find(Id);

            if (data != null)
            {
                db.Tbl_Category.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("AllCategories", "Admin");
        }

        public IActionResult AllProjects()
        {
            var projects = db.Tbl_Project.Include(x => x.Categories);
            return View(projects);
        }

        public IActionResult AddProject()
        {
            ViewBag.GetCategories = db.Tbl_Category.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(ProjectVM projectvm)
        {
            if (ModelState.IsValid)
            {
                string ImageName = projectvm.ProjectImage.FileName.ToString();
                var FolderPath = Path.Combine(env.WebRootPath, "images\\projects");
                var CompletePath = Path.Combine(FolderPath, ImageName);
                projectvm.ProjectImage.CopyTo(new FileStream(CompletePath, FileMode.Create));

                Project project = new Project();
                project.ProjectName = projectvm.ProjectName;
                project.ProjectDescription = projectvm.ProjectDescription;
                project.ProjectLink = projectvm.ProjectLink;
                project.ProjectImage = ImageName;
                project.CategoryId = Convert.ToInt32(projectvm.CategoryId);

                db.Tbl_Project.Add(project);
                db.SaveChanges();

                return RedirectToAction("AllProjects", "Admin");
            }
            return View();
        }

        public IActionResult UpdateProject(int Id)
        {
            var data = db.Tbl_Project.Find(Id);

            ViewBag.GetCategories = db.Tbl_Category.ToList();

            ProjectVM project = new ProjectVM();
            project.ProjectName = data.ProjectName;
            project.ProjectDescription = data.ProjectDescription;
            project.ProjectLink = data.ProjectLink;
            ViewData["ProjectImage"] = data.ProjectImage;
            project.CategoryId = Convert.ToInt32(data.CategoryId);

            return View(project);
        }

        [HttpPost]
        public IActionResult UpdateProject(ProjectVM projectvm, string? OldPic)
        {
            ViewBag.GetCategories = db.Tbl_Category.ToList();

            if (ModelState.IsValid)
            {
                string ImageName;

                if (projectvm.ProjectImage != null)
                {
                    ImageName = projectvm.ProjectImage.FileName.ToString();
                    var FolderPath = Path.Combine(env.WebRootPath, "images\\projects");
                    var CompletePath = Path.Combine(FolderPath, ImageName);
                    projectvm.ProjectImage.CopyTo(new FileStream(CompletePath, FileMode.Create));
                }
                else
                {
                    ImageName = OldPic;
                }

                Project project = new Project();
                project.Id = projectvm.Id;
                project.ProjectName = projectvm.ProjectName;
                project.ProjectDescription = projectvm.ProjectDescription;
                project.ProjectLink = projectvm.ProjectLink;
                project.ProjectImage = ImageName;
                project.CategoryId = Convert.ToInt32(projectvm.CategoryId);

                db.Tbl_Project.Update(project);
                db.SaveChanges();

                return RedirectToAction("AllProjects", "Admin");
            }
            return View();
        }

        public IActionResult DeleteProject(int Id)
        {
            var data = db.Tbl_Project.Find(Id);

            if (data != null)
            {
                db.Tbl_Project.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("AllProjects", "Admin");
        }

        public IActionResult Contact()
        {
            var data = db.Tbl_Contact;
            return View(data);
        }

        public IActionResult DeleteContact(int Id)
        {
            var data = db.Tbl_Contact.Find(Id);

            if (data != null)
            {
                db.Tbl_Contact.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("Contact", "Admin");
        }
    }
}
