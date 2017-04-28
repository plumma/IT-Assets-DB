using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITAssetsDatabase.Controllers.Partial
{
    public class LoggedInController : Controller
    {
     
        [ChildActionOnly]
        public string LoggedIn()
          {
            int hour = DateTime.Now.Hour;
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var message = hour < 12 ? "Good morning " + user : "Good afternoon " + user;
            return message;
        
        }

    }
}
