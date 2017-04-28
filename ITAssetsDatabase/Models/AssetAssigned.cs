using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class AssetAssigned
    {
        public int Id { get; set; }
        

        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        private DateTime? createdDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime CreateDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }



        public int? StaffId { get; set; }
        public virtual Staff Staff { get; set; } 
         
        public int AssetStatusID { get; set; }
        public virtual AssetStatus AssetStatus { get; set; }

        public int? AssetAssigneeID { get; set; }
        public virtual AssetAssignee AssetAssignee { get; set; }


        public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }

        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string Notes { get; set; }
         

    }  

}