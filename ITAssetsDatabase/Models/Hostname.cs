using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class Hostname
    {

        public Hostname()
        
        {
          this.Assets = new HashSet<Asset>();
                       
        }
        
                
        public int Id { get; set; }        

        public string LocationCode { get; set; }
        public string Number { get; set; }

        //[NotMapped]
        //public string FullName { get { return LocationCode + Number; } }
        public string FullHostname { get; set; }

        public string Location { get; set; } 
        public int StaffId { get; set; }
        public string EndUserId { get; set; }
        public int ProductTypeId  { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
} 