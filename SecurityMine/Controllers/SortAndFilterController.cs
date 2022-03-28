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
    public class SortAndFilterController : Controller
    {
        public AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        [HttpPost]
        public ActionResult MedicineSorting(string SortParameter)
        {
            AppIdentityDbContext context = new AppIdentityDbContext();
            AddMedicineValidation dispobj;
            List<AddMedicineValidation> list = new List<AddMedicineValidation>();

            string id = User.Identity.GetUserId();

            if (SortParameter.Equals("StockAsc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby s.Stock
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Stock(Low To High)";
            }
            else if (SortParameter.Equals("StockDesc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby s.Stock descending
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Stock(High To Low)";
            }
            else if (SortParameter.Equals("PriceAsc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.Price
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Price(Low To High)";
            }
            else if (SortParameter.Equals("PriceDesc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.Price descending
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Price(High To Low)";
            }
            else if (SortParameter.Equals("ExpiryAsc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.Expiry
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Expiry(Early First)";
            }
            else if (SortParameter.Equals("ExpiryDesc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.Expiry descending
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Expiry(Late First)";
            }
            else if (SortParameter.Equals("MedicineNameAsc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.MedicineName
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Medicine Name(A-Z)";
            }
            else if (SortParameter.Equals("MedicineNameDesc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.MedicineName descending
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Medicine Name(Z-A)";
            }
            else if (SortParameter.Equals("MedicineTypeAsc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.MedicineType
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Medicine Type(A-Z)";
            }
            else if (SortParameter.Equals("MedicineTypeDesc"))
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id
                              orderby m.MedicineType descending
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                //ViewBag.Data = result;
                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
                ViewBag.SortingDoneBy = "Medicine Type(Z-A)";
            }


            return View(list);
        }

        public ActionResult FilterMedicine()
        {
            return View();
        }


        public ActionResult FilterPerform(string MedicineName,int Stock,float Price,DateTime Expiry,string MedicineType,string StockAttribute)
        {
            AppIdentityDbContext context = new AppIdentityDbContext();
            AddMedicineValidation dispobj;
            List<AddMedicineValidation> list = new List<AddMedicineValidation>();
            string id = User.Identity.GetUserId();

            if(MedicineName.Equals("Medicine Name")==false)
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id && m.MedicineName==MedicineName
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
            }
            else if (MedicineType.Equals("Medicine Type") == false)
            {
                var result = (from m in context.Medicines
                              join s in context.StoreManagements
                              on m.MedicineId equals s.MedicineId
                              where m.UserId == id && m.MedicineType==MedicineType
                              select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                foreach (var r in result)
                {
                    dispobj = new AddMedicineValidation();
                    dispobj.MedicineName = r.MedicineName;
                    dispobj.MedicineType = r.MedicineType;
                    dispobj.Expiry = r.Expiry;
                    dispobj.Price = r.Price;
                    dispobj.Stock = r.Stock;

                    list.Add(dispobj);
                }
            }
            else if (Stock!=0)
            {
                if(StockAttribute.Equals("EqualTo"))
                {
                    var result = (from m in context.Medicines
                                  join s in context.StoreManagements
                                  on m.MedicineId equals s.MedicineId
                                  where m.UserId == id && s.Stock==Stock
                                  select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                    foreach (var r in result)
                    {
                        dispobj = new AddMedicineValidation();
                        dispobj.MedicineName = r.MedicineName;
                        dispobj.MedicineType = r.MedicineType;
                        dispobj.Expiry = r.Expiry;
                        dispobj.Price = r.Price;
                        dispobj.Stock = r.Stock;

                        list.Add(dispobj);
                    }
                }
                else if (StockAttribute.Equals("GreaterThan"))
                {
                    var result = (from m in context.Medicines
                                  join s in context.StoreManagements
                                  on m.MedicineId equals s.MedicineId
                                  where m.UserId == id && s.Stock>Stock
                                  select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                    foreach (var r in result)
                    {
                        dispobj = new AddMedicineValidation();
                        dispobj.MedicineName = r.MedicineName;
                        dispobj.MedicineType = r.MedicineType;
                        dispobj.Expiry = r.Expiry;
                        dispobj.Price = r.Price;
                        dispobj.Stock = r.Stock;

                        list.Add(dispobj);
                    }
                }
                else if (StockAttribute.Equals("GreaterThanEqual"))
                {
                    var result = (from m in context.Medicines
                                  join s in context.StoreManagements
                                  on m.MedicineId equals s.MedicineId
                                  where m.UserId == id && s.Stock >= Stock
                                  select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                    foreach (var r in result)
                    {
                        dispobj = new AddMedicineValidation();
                        dispobj.MedicineName = r.MedicineName;
                        dispobj.MedicineType = r.MedicineType;
                        dispobj.Expiry = r.Expiry;
                        dispobj.Price = r.Price;
                        dispobj.Stock = r.Stock;

                        list.Add(dispobj);
                    }
                }
                else if (StockAttribute.Equals("LessThan"))
                {
                    var result = (from m in context.Medicines
                                  join s in context.StoreManagements
                                  on m.MedicineId equals s.MedicineId
                                  where m.UserId == id && s.Stock < Stock
                                  select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                    foreach (var r in result)
                    {
                        dispobj = new AddMedicineValidation();
                        dispobj.MedicineName = r.MedicineName;
                        dispobj.MedicineType = r.MedicineType;
                        dispobj.Expiry = r.Expiry;
                        dispobj.Price = r.Price;
                        dispobj.Stock = r.Stock;

                        list.Add(dispobj);
                    }
                }
                else if (StockAttribute.Equals("LessThanEqual"))
                {
                    var result = (from m in context.Medicines
                                  join s in context.StoreManagements
                                  on m.MedicineId equals s.MedicineId
                                  where m.UserId == id && s.Stock <= Stock
                                  select new { m.MedicineName, m.MedicineType, m.Expiry, m.Price, s.Stock, s.UserId }).ToList();

                    foreach (var r in result)
                    {
                        dispobj = new AddMedicineValidation();
                        dispobj.MedicineName = r.MedicineName;
                        dispobj.MedicineType = r.MedicineType;
                        dispobj.Expiry = r.Expiry;
                        dispobj.Price = r.Price;
                        dispobj.Stock = r.Stock;

                        list.Add(dispobj);
                    }
                }
            }



            return View(list);

        }

    }
}