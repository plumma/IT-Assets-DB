using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class AssetAssignee
    {

        public AssetAssignee()
        {
            this.AssetAssigned = new HashSet<AssetAssigned>();
        

        }
         
        public virtual ICollection<AssetAssigned> AssetAssigned { get; set; }
        
        
        public int Id { get; set; }
        public string HelpdeskRef { get; set; }

        [ForeignKey("Requester"), Column(Order = 0)]
        public int? RequesterId { get; set; }
        public virtual Employee Requester { get; set; }

        [ForeignKey("Assignee"), Column(Order = 1)]
        public int? AssigneeId { get; set; }
        public virtual Employee Assignee { get; set; }
         
         
        //public int AssetId { get; set; }
        public string SignOffSheetFileName { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        private DateTime? createdDate { get; set; }

       
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? CreateDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }





        
         

    }
}