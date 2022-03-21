using RESTServiceMine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTServiceMine.Controllers
{
    public class LoginController : ApiController
    {
        UserLoginModel model;

        public LoginController()
        {
            model = new UserLoginModel();
        }
        
        public List<AspNetUser> GetUsers()
        {
            List<AspNetUser> list = model.AllUsers();
            return list;
        }
    }
}
