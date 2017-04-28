using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class Device
    {
        public Device()
        {
            this.Assets = new HashSet<Asset>();         
        }
          


        public int Id { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }   
    }
}