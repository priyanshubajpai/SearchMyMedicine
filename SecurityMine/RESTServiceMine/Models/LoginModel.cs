using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServiceMine.Models
{
    public class UserLoginModel
    {
        GenpactIdentityDbEntities context;

        public UserLoginModel()
        {
            context = new GenpactIdentityDbEntities();
        }

        public List<AspNetUser> AllUsers()
        {
            List<AspNetUser> list = new List<AspNetUser>();
            list = context.AspNetUsers.ToList();
            return list;
        }
    }

    
}