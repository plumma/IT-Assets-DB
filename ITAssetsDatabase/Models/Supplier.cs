using System;
using System.Collections.Generic;

namespace ITAssetsDatabase.Models
{

    public class Supplier
    {
        public Supplier()
        {
            this.SupplierContacts = new HashSet<SupplierContact>();
        }
    
        public int Id { get; set; }
        public string Company { get; set; }
        public int ProductMakeId { get; set; }
        public int? AddressId { get; set; }
    
        public virtual ProductMake ProductMake { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }
    }
}
