using System;
using System.Collections.Generic;

namespace ITAssetsDatabase.Models
{
    
    public class Product
    {
  
        public int Id { get; set; }
        public string Model { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductMakeId { get; set; }
        public string Barcode { get; set; }  // Type Barcode
        public string SerialNo { get; set; }
    
    
        public virtual ProductType ProductType { get; set; }
        public virtual ProductMake ProductMake { get; set; }
    }
}
 