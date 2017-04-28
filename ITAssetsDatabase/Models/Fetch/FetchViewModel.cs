using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  // SelectList

namespace ITAssetsDatabase.Models.Fetch
{
    public class FetchViewModel
    {

        public SelectList LocationID { get; set; }
        public SelectList MyProperty { get; set; }
        public string Description { get; set; }
        public SelectList StaffId { get; set; } 
    }
}