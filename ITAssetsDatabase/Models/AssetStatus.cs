using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class AssetStatus
    {

        public AssetStatus()
        {
            this.AssetsAssigned = new HashSet<AssetAssigned>();
         
        }


        public int Id { get; set; }
        public string State { get; set; }

        public virtual ICollection<AssetAssigned> AssetsAssigned { get; set; }
          
    }
}