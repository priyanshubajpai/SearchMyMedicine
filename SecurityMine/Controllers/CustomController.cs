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

                AppUser appUser = new AppUser() {UserName="ABC", Email ="abc@gmail.com", PhoneNumber = "12345"};
                appUser.Addresses.Add(address);

                //AppIdentityDbContext context = new AppIdentityDbContext();
                //context.Addresses.Add(address);

                //CustomContext context = new CustomContext();
                //context.Addresses.Add(address);

                return View("~/Views/Admin/Thanks.cshtml", obj);
            }
        }
    }
}