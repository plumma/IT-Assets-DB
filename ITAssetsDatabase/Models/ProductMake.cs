using System;
using System.Collections.Generic;

namespace ITAssetsDatabase.Models
{

    public class ProductMake
    {
        public ProductMake()
        {
            this.Products = new HashSet<Product>();
            this.Suppliers = new HashSet<Supplier>();
        }
    
        public int Id { get; set; }
        public string Make { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
