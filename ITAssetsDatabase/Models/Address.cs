using System;
using System.Collections.Generic;

namespace ITAssetsDatabase.Models
{
  
    public class Address
    {
        public Address()
        {
            this.Suppliers = new HashSet<Supplier>();
            this.BusinessAreas = new HashSet<BusinessArea>();
        }
    
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
    
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<BusinessArea> BusinessAreas { get; set; }
    }
}
