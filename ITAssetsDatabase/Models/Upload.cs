using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;  // For HttpPostedFileBase


namespace ITAssetsDatabase.Models
{
    public class Upload
    {
        //public Upload()
        //{
        //    this.UploadedFiles = new HashSet<UploadedFile>();

        //}


        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        private DateTime? createdDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }


        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string InstallLocation { get; set; }
        
        public bool? Packaged { get; set; }
        public string Notes { get; set; }

        
        public virtual ICollection<UploadedFile> UploadedFiles { get; set; }



    }
}