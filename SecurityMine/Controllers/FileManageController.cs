using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using SecurityMine.Models;
using Microsoft.AspNet.Identity;

namespace SecurityMine.Controllers
{
    public class FileManageController : Controller
    {
        public ActionResult GetDeletedUsers()
        {
            FileManagement obj = new FileManagement();
            List<string> list=obj.ReadDeletedUsers();
            ViewBag.Data = list;
            return View();
        }

        public ActionResult SendMessageToUser(string UserName)
        {
            ViewBag.UserName = UserName;
            SendMessageValidation obj = new SendMessageValidation();
            return View(obj);
        }

        public ActionResult SendMessageToAdmin(ReadMessagesValidation readobj)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/Home/MasterAdminLogin.cshtml", readobj);
            }
            else
            {
                ViewBag.UserName = readobj.UserName;
                SendMessageValidation obj = new SendMessageValidation();
                return View(obj);
            }
        }

        public ActionResult SendMyMessage(SendMessageValidation obj)
        {
            if(ModelState.IsValid==false)
            {
                return View("~/Views/FileManage/SendMessageToUser.cshtml", obj);
            }
            else
            {
                FileManagement fileobj = new FileManagement();
                fileobj.WriteMessages(obj);
                return View("~/Views/Admin/Thanks.cshtml");
            }
        }

        public ActionResult SendMyMessageToAdmin(SendMessageValidation obj)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/FileManage/SendMessageToAdmin.cshtml", obj);
            }
            else
            {
                FileManagement fileobj = new FileManagement();
                fileobj.WriteMessagesAsUser(obj);
                fileobj.WriteMessagesAsUserToMasterAdminFile(obj);
                
                return View("~/Views/Admin/Thanks.cshtml");
            }
        }

        public ActionResult GetMyMessageAsAdmin(ReadMessagesValidation readobj)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/Home/MasterAdminLogin.cshtml");
               
            }
            else
            {
                FileManagement obj = new FileManagement();
                List<string> list = obj.ReadMyMessages(readobj);
                if (list == null)
                {
                    ViewBag.NoSuchUserPresent = readobj.UserName;
                    return View("~/Views/Home/MasterAdminLogin.cshtml");
                }
                ViewBag.Data = list;
                return View();
            }
        }

        public ActionResult GetMyMessageAsUser(ReadMessagesValidation readobj)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/Home/MasterAdminLogin.cshtml",readobj);

            }
            else
            { 
                FileManagement obj = new FileManagement();
                List<string> list = obj.ReadMyMessages(readobj);
                if(list==null)
                {
                    ViewBag.NoSuchUserPresent = readobj.UserName;
                    return View("~/Views/Home/MasterAdminLogin.cshtml");
                }
                ViewBag.Data = list;
                return View();
            }
        }
    }

    
}