using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class Build
    {
        public Build()
        {
            this.Assets = new HashSet<Asset>();         
        }

        public int Id { get; set; }
        public string Domain { get; set; }
        public string BuildName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}