using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SecurityMine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SecurityMine.Controllers
{
    public class AdminController : Controller
    {
        public AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        public AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl ?? "/Home/Index";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                returnUrl = returnUrl ?? "/Home/Index";
                AppUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    //A claim is a piece of information about the user, along with some information about where the information came from
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationProperties property = new AuthenticationProperties() { IsPersistent = model.RememberMe };
                    AuthenticationManager.SignIn(property, claim);
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "Invalid User");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Admin");
        }

        async Task CreateUsers()
        {
            AppUser user = new AppUser() { UserName = "Jack", Email = "jack@gmail.com", PhoneNumber = "112" };
            IdentityResult result = await UserManager.CreateAsync(user, "alice@123");

            //user = new AppUser() { UserName = "Mohan", Email = "mohan@gmail.com", PhoneNumber = "76543" };
            //result = await UserManager.CreateAsync(user, "mohan@123");

        }
        public async Task<ActionResult> AssignRoles()
        {
            if (RoleManager.Roles.Count() == 0)
            {
                await CreateUsers();
                var r = new AppRole() { Name = "Administrator" };
                await RoleManager.CreateAsync(r);
                //RoleManager.Create(new AppRole() { Name = "Executive" });

                AppUser user = UserManager.FindByName("Jack");
                UserManager.AddToRole(user.Id, "Administrator");

                //user = UserManager.FindByName("Mohan");
                //UserManager.AddToRole(user.Id, "Executive");


            }
            return Redirect("/Home/Index");
        }


        //async Task CreateMasterUser()
        //{
        //    AppUser user = new AppUser() { UserName = "jack", Email = "jack@gmail.com", PhoneNumber = "9936891809" };
        //    IdentityResult result = await UserManager.CreateAsync(user, "alice");
        //}
        //public async Task<ActionResult> CreateMasterAdmin()
        //{

        //    await CreateMasterUser();
        //    var r = new AppRole() { Name = "Administrator" };
        //    await RoleManager.CreateAsync(r);

        //    AppUser user = UserManager.FindByName("jack");
        //    UserManager.AddToRole(user.Id, "Administrator");

        //    return Redirect("/Home/Index");
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddUser(AppUser u, string Password, string RetypePassword)
        //{
        //    AppUser user = new AppUser() { UserName = u.UserName, Email = u.Email, PhoneNumber = u.PhoneNumber };

        //    if (Password.Equals(RetypePassword))
        //    {
        //        IdentityResult result = await UserManager.CreateAsync(user, Password);

        //        if (result.Succeeded)
        //        {
        //            if (RoleManager.Roles.Count() == 0)
        //            {
        //                var r = new AppRole() { Name = "MedicalStore" };
        //                await RoleManager.CreateAsync(r);
        //            }

        //            user = UserManager.FindByName(u.UserName);
        //            UserManager.AddToRole(user.Id, "MedicalStore");

        //            return View("Thanks");
        //        }
        //        else
        //        {
        //            return Redirect("/Home/MasterAdmin");
        //        }
        //    }
        //    else
        //    {
        //        return Redirect("/Home/MasterAdmin");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> AddUser(NewAspUser newuser, string Password, string RetypePassword)
        {
            if(ModelState.IsValid==false)
            {
                return View("~/Views/Home/MasterAdmin.cshtml", newuser);
            }
            else
            {
                AppUser user1 = new AppUser();
                user1.UserName = newuser.UserName;
                user1.Email = newuser.Email;
                user1.PhoneNumber = newuser.PhoneNumber;
                Password = newuser.Password;
                RetypePassword = newuser.RetypePassword;
                AppUser user = new AppUser() { UserName = user1.UserName, Email = user1.Email, PhoneNumber = user1.PhoneNumber };

                if (Password.Equals(RetypePassword))
                {
                    IdentityResult result = await UserManager.CreateAsync(user, Password);

                    if (result.Succeeded)
                    {
                        if (RoleManager.Roles.Count() == 0)
                        {
                            var r = new AppRole() { Name = "MedicalStore" };
                            await RoleManager.CreateAsync(r);
                        }

                        user = UserManager.FindByName(user1.UserName);
                        UserManager.AddToRole(user.Id, "MedicalStore");

                        return View("Thanks");
                    }
                    else
                    {
                        return Redirect("/Home/MasterAdmin");
                    }
                }
                else
                {
                    return Redirect("/Home/MasterAdmin");
                }
            }
        }

        public ActionResult ChangePasswd()
        {
            ChangePasswordValidation obj = new ChangePasswordValidation();
            return View(obj);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordValidation obj)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/Admin/ChangePasswd.cshtml", obj);
            }
            else
            {
                string Current_Password = obj.Current_Password;
                string New_Password = obj.New_Password;
                string Retype_Password = obj.Retype_Password;

                if (New_Password.Equals(Retype_Password))
                {

                    string id = User.Identity.GetUserId();
                    AppUser user = UserManager.FindById(id);
                    IdentityResult result = UserManager.ChangePassword(user.Id, Current_Password, New_Password);

                    if (result.Succeeded)
                    {
                        return View("Thanks");
                    }
                    else
                    {
                        return View("~/Views/Admin/ChangePasswd.cshtml" +
                            "",obj);
                    }
                }
                else
                {
                    return View("~/Views/Admin/ChangePasswd.cshtml");
                }
            
            }
        }

        public List<AppUser> GetUsers()
        {
            return UserManager.Users.ToList();
        }

        //public List<AppRole> GetRoles()
        //{
        //    return RoleManager.Roles.ToList();
        //}

        public ActionResult DisplayAllUsers()
        {
            List<AppUser> list = GetUsers();
            return View(list);
        }

        public ActionResult DeleteUser(string id)
        {
            FileManagement fileobj = new FileManagement();
            AppUser user = UserManager.FindById(id);
            fileobj.WriteDeletedUser(user);
            IdentityResult result = UserManager.Delete(user);

            if (result.Succeeded)
            {
                return View("Thanks");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult ResetPasswd()
        {
            ResetPasswordValidation obj = new ResetPasswordValidation();
            return View(obj);
        }

        public ActionResult ResetPasswordAfterLogin(string UserName)
        {
            ResetPasswordValidation obj = new ResetPasswordValidation();
            ViewBag.UserName = UserName;
            return View(obj);
        }

        public ActionResult ResetPassword(ResetPasswordValidation obj)
        {
            
            if(ModelState.IsValid==false)
            {
                return View("ResetPasswd", obj);
            }
            else
            {
                string UserName = obj.UserName;
                string New_Password = obj.New_Password;
                string Retype_Password = obj.Retype_Password;

                AppUser user = UserManager.FindByName(UserName);

                if (user != null)
                {
                    //string prev_Hash = user.PasswordHash;
                    //string new_Hash = UserManager.PasswordHasher.HashPassword(New_Password);

                    if (New_Password.Equals(Retype_Password))
                    {
                        var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("AppIdentityDbContext");
                        UserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(provider.Create("PasswordReset"));
                        var token = UserManager.GeneratePasswordResetToken(user.Id);


                        IdentityResult passwdchangeresult = UserManager.ResetPassword(user.Id, token, New_Password);

                        if (passwdchangeresult.Succeeded)
                        {
                            return View("Thanks");
                        }
                        else
                        {
                            return View("ResetPasswd", obj);
                        }
                    }
                    else
                    {
                        return View("ResetPasswd");
                    }
                }
                else
                {
                    return View("ResetPasswd");
                }
            }
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}