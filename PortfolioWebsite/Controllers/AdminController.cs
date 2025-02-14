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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                ViewData["TotalCategories"] = db.Tbl_Category.Count();
                ViewData["TotalMessages"] = db.Tbl_Contact.Count();
                ViewData["TotalProjects"] = db.Tbl_Project.Count();
                ViewData["TotalSkills"] = db.Tbl_Skill.Count();
                ViewData["TotalServices"] = db.Tbl_Service.Count();

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult PersonalInfo()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

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
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                var service = db.Tbl_Service;
                return View(service);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult AddService()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                var data = db.Tbl_Service.Find(id);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                var skills = db.Tbl_Skill;
                return View(skills);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult AddSkill()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                var data = db.Tbl_Skill.Find(id);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkill(Skill skill)
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                var category = db.Tbl_Category;
                return View(category);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult AddCategory()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                DisplayUserData();

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                var data = db.Tbl_Category.Find(id);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                var projects = db.Tbl_Project.Include(x => x.Categories);
                return View(projects);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult AddProject()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                ViewBag.GetCategories = db.Tbl_Category.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

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
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                var data = db.Tbl_Contact;
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
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

        public IActionResult AllUsers()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                var users = db.Tbl_User;
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult AddUser()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                string ImageName = userVM.Image.FileName.ToString();
                var FolderPath = Path.Combine(env.WebRootPath, "images\\users");
                var CompletePath = Path.Combine(FolderPath, ImageName);
                userVM.Image.CopyTo(new FileStream(CompletePath, FileMode.Create));

                User user = new User();
                user.UserName = userVM.UserName;
                user.Password = userVM.Password;
                user.Image = ImageName;

                db.Tbl_User.Add(user);
                db.SaveChanges();

                return RedirectToAction("AllUsers", "Admin");
            }
            return View();
        }

        public IActionResult UpdateUser(int Id)
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                DisplayUserData();

                var data = db.Tbl_User.Find(Id);

                UserVM user = new UserVM();
                user.Id = data.Id;
                user.UserName = data.UserName;
                user.Password = data.Password;
                ViewData["UserImage"] = data.Image;

                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(UserVM uservm, string? OldPic)
        {
            if (ModelState.IsValid)
            {
                string ImageName;

                if (uservm.Image != null)
                {
                    ImageName = uservm.Image.FileName.ToString();
                    var FolderPath = Path.Combine(env.WebRootPath, "images\\users");
                    var CompletePath = Path.Combine(FolderPath, ImageName);
                    uservm.Image.CopyTo(new FileStream(CompletePath, FileMode.Create));
                }
                else
                {
                    ImageName = OldPic;
                }

                User user = new User();
                user.Id = uservm.Id;
                user.UserName = uservm.UserName;
                user.Password = uservm.Password;
                user.Image = ImageName;

                db.Tbl_User.Update(user);
                db.SaveChanges();

                return RedirectToAction("AllUsers", "Admin");
            }
            return View();
        }

        public IActionResult DeleteUser(int Id)
        {
            var data = db.Tbl_User.Find(Id);

            if (data != null)
            {
                db.Tbl_User.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("AllUsers", "Admin");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var data = db.Tbl_User.Where(x => x.UserName.Equals(login.UserName) && x.Password.Equals(login.Password)).FirstOrDefault();

                if (data != null)
                {
                    HttpContext.Session.SetInt32("UserId", data.Id);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

        public void DisplayUserData()
        {
            ViewBag.Profile = db.Tbl_User.Where(x => x.Id == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
        }
    }
}
