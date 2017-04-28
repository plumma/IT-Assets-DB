using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models.Assets
{
    public class AssetsViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CreateDate { get; set; }
        public int EndUserNameId { get; set; }
        public string EndUserName { get; set; }
        public string HelpdeskRef { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
  
         
    }
}