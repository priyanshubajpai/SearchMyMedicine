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
            return View();
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
            ViewBag.Message = "Contact page.";

            return View();
        }

        
        public ActionResult MasterAdmin()
        {
            NewAspUser newuser = new NewAspUser();
            return View(newuser);
        }

        [Authorize(Roles = "Administrator,MedicalStore")]
        public ActionResult MasterAdminLogin()
        {
            string id = User.Identity.GetUserId();
            AppUser user = UserManager.FindById(id);
            ViewBag.DisplayUserName = user.UserName;

            ViewBag.NoSuchUserPresent = null;

            FileManagement obj = new FileManagement();
            String prevlength = obj.ReadLastLineInAdminMessageSizeFile();
           
            string path = @"C:\Users\850058620\Desktop\BookMyMedicine\BookMyMedicine\MessageExchange\Admin.txt";
            long length = new System.IO.FileInfo(path).Length;
            obj.WriteFileSize(length);

            if(prevlength.Equals(length.ToString())==false)
            {
                ViewBag.New = "yes";
            }


            //var list=UserManager.Users.ToList();
            //if (list != null)
            //{
            //    ViewBag.userdata = list;
            //}
            //ViewBag.UserList = new SelectList(UserManager.Users.ToList(), "UserName", "UserName");

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