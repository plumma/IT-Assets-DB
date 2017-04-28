namespace ITAssetsDatabase.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}
