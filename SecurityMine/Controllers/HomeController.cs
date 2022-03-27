using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SecurityMine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityMine.Controllers
{
    public class HomeController : Controller
    {
       
        public AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        public ActionResult Index()
        {
            SearchMedicineValidation obj = new SearchMedicineValidation();
            return View(obj);
        }
        

        public ViewResult Home()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            FeedBackValidation obj = new FeedBackValidation();
            return View(obj);
        }

        
        public ActionResult MasterAdmin()
        {
            NewAspUser newuser = new NewAspUser();
            return View(newuser);
        }

        [Authorize(Roles = "Administrator,MedicalStore,Suspended")]
        public ActionResult MasterAdminLogin()
        {
            string id = User.Identity.GetUserId();
            AppUser user = UserManager.FindById(id);
            ViewBag.DisplayUserName = user.UserName;

            ViewBag.NoSuchUserPresent = null;

            if (UserManager.IsInRole(id, "Administrator"))
            {
                FileManagement obj = new FileManagement();
                String prevlength = obj.ReadLastLineInAdminMessageSizeFile();

                string path = @"C:\Users\Hp\Desktop\BookMyMedicine\MessageExchange\Admin.txt";
                long length = new System.IO.FileInfo(path).Length;
                obj.WriteFileSize(length);

                if (prevlength.Equals(length.ToString()) == false)
                {
                    ViewBag.New = "yes";
                }
            }


            //var list=UserManager.Users.ToList();
            //if (list != null)
            //{
            //    ViewBag.userdata = list;
            //}
            //ViewBag.UserList = new SelectList(UserManager.Users.ToList(), "UserName", "UserName");

            ViewBag.UserId = id;

            return View();
        }

        //[Authorize(Roles = "MedicalStore,Administrator")]
        //public ActionResult MedicalStoreLogin()
        //{
        //    ViewBag.NoSuchUserPresent = null;
        //    string id = User.Identity.GetUserId();
        //    AppUser user = UserManager.FindById(id);
        //    ViewData["Name"] = user.UserName;
        //    ViewBag.DisplayUserName = user.UserName;
        //    return View();
        //}
        
    }
}