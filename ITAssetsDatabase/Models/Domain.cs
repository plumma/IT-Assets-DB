using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class Domain
    {
            public Domain()
        {
             this.Assets = new HashSet<Asset>();
        }



        public int Id { get; set; }
        public string DomainName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}