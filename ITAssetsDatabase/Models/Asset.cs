using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ITAssetsDatabase.Models
{
    public class Asset
    {

          public Asset()
        {
            this.AssetAssigned = new HashSet<AssetAssigned>();
            this.AssetSignoff = new HashSet<AssetSignoff>();     
        } 
         

        public int Id { get; set; }
        

        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        private DateTime? createdDate { get; set; }

       
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? CreateDate
        { 
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }


        public string PRRef { get; set; }
        public string PORef { get; set; }
        public string AssetNo { get; set; }
        public string SerialNo { get; set; }
        public string MAC_Address { get; set; }
        
        
        // Database Record of original Assigned User when hardware first ordered
        
        
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

               
  
        public string HelpdeskRef { get; set; }

        // ******************************
          
        public int? HostnameId { get; set; }   //  Many to One ->
        public virtual Hostname Hostname { get; set; }

                
        // Computer Account Base Domain Name
        public int? DomainNameId { get; set; }
        public virtual Domain DomainName { get; set; }
      
         
        public int? BuildId { get; set; }   //  Many to One ->
        public virtual Build Build { get; set; }
         

        public int? DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public int? StaffId { get; set; }
        public virtual Staff Staff { get; set; }
            
        public virtual ICollection<AssetAssigned> AssetAssigned { get; set; }    // One to many: Asset -> AssetAssigned

        public virtual ICollection<AssetSignoff> AssetSignoff { get; set; }    // One to many: Asset -> AssetSignoff
         
                
        
    }
}