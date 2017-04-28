using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class AssetSignoff
    {

        public int ID { get; set; }        
        public string Uploadpath { get; set; }
        
        public int EmployeeId  { get; set; }
        public virtual Employee Employee { get; set; }
         
        public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }
        

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