using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityMine.Models
{
    public class AppUser:IdentityUser
    {
        //Email,Phone,UserName
    }

    public class AppIdentityDbContext:IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext():base("IdentityDb")
        {

        }

        static AppIdentityDbContext()
        {

        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            UserStore<AppUser> store = new UserStore<AppUser>(db);
            AppUserManager manager = new AppUserManager(store);
            /*  manager.PasswordValidator = new PasswordValidator
              {
                  RequiredLength = 6,
                  RequireNonLetterOrDigit = false,
                  RequireDigit = false,
                  RequireLowercase = true,
                  RequireUppercase = true
              }; */
            return manager;
        }
    }

    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
    }

    public class AppRoleManager : RoleManager<AppRole>//, IDisposable
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store)
        {
        }
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            AppIdentityDbContext dbContext = context.Get<AppIdentityDbContext>();
            RoleStore<AppRole> store = new RoleStore<AppRole>(dbContext);
            AppRoleManager manager = new AppRoleManager(store);
            return manager;
        }
    }

    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Login"),
            });
        }
    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}