using System;
using System.Collections.Generic;

namespace ITAssetsDatabase.Models
{
    
    public class SupplierContact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string ContactTel { get; set; }
        public int SupplierId { get; set; }
    
        public virtual Supplier Supplier { get; set; }
    }
}
