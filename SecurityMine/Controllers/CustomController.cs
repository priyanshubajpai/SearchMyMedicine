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

        public ActionResult AddMedicine()
        {
            AddMedicineValidation obj = new AddMedicineValidation();
            return View(obj);
        }

        public ActionResult StoreMedicine(AddMedicineValidation obj)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/Custom/AddMedicine.cshtml", obj);
            }
            else
            {
                Medicine medicine = new Medicine();
                medicine.MedicineName = obj.MedicineName;
                medicine.MedicineType = obj.MedicineType;
                medicine.Expiry = obj.Expiry;
                medicine.Price = obj.Price;

                string id = User.Identity.GetUserId();

                medicine.UserId = id;

                AppIdentityDbContext context = new AppIdentityDbContext();
                var user_present = context.Medicines.Where(u => u.UserId == id).FirstOrDefault();

                if (user_present != null)
                {
                    var res = context.Medicines.Where(m => m.MedicineName == obj.MedicineName).SingleOrDefault(u => u.UserId == id);

                    if (res == null)
                    {
                        context.Medicines.Add(medicine);
                        context.SaveChanges();

                        var data = context.Medicines.Where(m => m.MedicineName == obj.MedicineName).SingleOrDefault(u => u.UserId == id);
                        int medid = data.MedicineId;
                        StoreManagement storeManagement = new StoreManagement();
                        storeManagement.Stock = obj.Stock;
                        storeManagement.MedicineId = medid;
                        storeManagement.UserId = id;

                        context.StoreManagements.Add(storeManagement);
                        context.SaveChanges();
                    }
                    else
                    {
                        int medid = res.MedicineId;
                        var ans = context.StoreManagements.Where(s => s.MedicineId == medid).SingleOrDefault(u => u.UserId == id);

                        context.Medicines.Remove(res);
                        context.Medicines.Add(medicine);
                        context.SaveChanges();

                        var data = context.Medicines.Where(m => m.MedicineName == obj.MedicineName).SingleOrDefault(u => u.UserId == id);
                        int mid = data.MedicineId;
                        StoreManagement storeManagement = new StoreManagement();
                        storeManagement.Stock = obj.Stock;
                        storeManagement.MedicineId = mid;
                        storeManagement.UserId = id;

                        context.StoreManagements.Add(storeManagement);
                        context.SaveChanges();

                    }
                    return View("~/Views/Admin/Thanks.cshtml", obj);
                }
                else
                {
                    context.Medicines.Add(medicine);
                    context.SaveChanges();

                    var data = context.Medicines.Where(m => m.MedicineName == obj.MedicineName).SingleOrDefault(u=>u.UserId==id);
                    int medid = data.MedicineId;
                    StoreManagement storeManagement = new StoreManagement();
                    storeManagement.Stock = obj.Stock;
                    storeManagement.MedicineId = medid;
                    storeManagement.UserId = id;

                    context.StoreManagements.Add(storeManagement);
                    context.SaveChanges();

                    return View("~/Views/Admin/Thanks.cshtml", obj);
                }
            }
        }

        public ActionResult DisplayMedicineList()
        {
            string id = User.Identity.GetUserId();
            AppIdentityDbContext context = new AppIdentityDbContext();
            var result = (from m in context.Medicines
                          join s in context.StoreManagements
                          on m.MedicineId equals s.MedicineId
                          where m.UserId==id
                          select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock }
                        ).ToList();

            AppUser user = UserManager.FindById(id);
            ViewBag.Name = user.UserName;

            AddMedicineValidation dispobj;
            List<AddMedicineValidation> list = new List<AddMedicineValidation>();

            foreach (var r in result)
            {
                dispobj= new AddMedicineValidation();
                dispobj.MedicineName = r.MedicineName;
                dispobj.MedicineType = r.MedicineType;
                dispobj.Expiry = r.Expiry;
                dispobj.Price = r.Price;
                dispobj.Stock = r.Stock;

                list.Add(dispobj);
            }
            ViewBag.List = result;
            return View(list);
        }

        public ActionResult SearchMedicine(SearchMedicineValidation obj)
        {
            if(ModelState.IsValid==false)
            {
                return View("~/Views/Home/Index.cshtml", obj);
            }
            else
            {
                String med = obj.SearchKeyword;
                AppIdentityDbContext context = new AppIdentityDbContext();
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.MedicineName == med
                              join a in context.Addresses
                              on s.UserId equals a.UserId
                              select new {m.MedicineType,m.Expiry,m.Price,s.Stock,a.AddressLine,a.District}
                              );

                if(result==null)
                {
                    return View("Error");
                }
                else
                {
                    ViewBag.Data = result;
                    return View();
                }
            }
        }
    }
}