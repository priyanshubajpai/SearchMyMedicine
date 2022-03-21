using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SecurityMine.Models;

namespace SecurityMine.Controllers
{
    public class CustomController : Controller
    {
        //AppUser appUser;
        //public CustomController()
        //{
        //    appUser = new AppUser();
        //}
        public AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        public ActionResult StoreAddress()
        {
            StoreAddressValidation obj = new StoreAddressValidation();
            return View(obj);
        }


        [HttpPost]
        public ActionResult AddAddress(StoreAddressValidation obj)
        {
            if(ModelState.IsValid==false)
            {
                return View("~/Views/Custom/StoreAddress.cshtml", obj);
            }
            else
            {
                Address address = new Address();
                address.AddressLine = obj.AddressLine;
                address.District = obj.District;
                address.City = obj.City;
                address.PinCode = obj.PinCode;
                address.State = obj.State;
                address.Country = obj.Country;

                string id = User.Identity.GetUserId();

                address.UserId = id;

                //AppUser appUser = new AppUser();
                //appUser.Addresses.Add(address);

                AppIdentityDbContext context = new AppIdentityDbContext();

                var res=context.Addresses.SingleOrDefault(adrs=>adrs.UserId==id);
                if(res==null)
                {
                    context.Addresses.Add(address);
                    context.SaveChanges();
                }
                else
                {
                    context.Addresses.Remove(res);
                    context.Addresses.Add(address);
                    context.SaveChanges();
                }

                
                return View("~/Views/Admin/Thanks.cshtml", obj);
            }
        }

        public ActionResult DisplayContactDetail(string id)
        {
            AppIdentityDbContext context = new AppIdentityDbContext();

            var res = context.Addresses.SingleOrDefault(adrs => adrs.UserId == id);
            ViewBag.AddressLine = res.AddressLine;
            ViewBag.Dist = res.District;
            ViewBag.City = res.City;
            ViewBag.Pin = res.PinCode;
            ViewBag.State = res.State;
            ViewBag.Country = res.Country;

            AppUser user = UserManager.FindById(id);
            ViewBag.Name = user.UserName;
            //ViewBag.Info = res.AddressLine + " " + res.District + " " + res.City + " " + res.PinCode + " " + res.State + " " + res.Country;
            return View();
        }
    }
}