using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class ADLogon
    {

        public int id { get; set; }
        public string Description { get; set; }
        public string AD_Rights { get; set; }
        public string Domain { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
          
    }
}