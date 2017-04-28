using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class Location
    {
        public Location()
        {
            this.AssetAssigned = new HashSet<AssetAssigned>();         
        }


        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string Town { get; set; }

        [NotMapped]
        public string FullLocation { get { return BuildingName + ", " + Town; } }

        public string LocationCode { get; set; }

        public virtual ICollection<AssetAssigned> AssetAssigned { get; set; }


    }
} 