using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITAssetsDatabase.Models;

namespace ITAssetsDatabase.Models.Assets
{
    public class DetailsViewModel
    {
        public Asset Asset { get; set; }       
        public ICollection<AuditClass> Audit { get; set; }
    } 
}