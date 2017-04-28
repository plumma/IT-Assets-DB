using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITAssetsDatabase.Models
{
    public class Article
    {
       
        public int id { get; set; }
        public string Summary  { get; set; }
        public string Details { get; set; }
        public string Resolution { get; set; }
        
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> CreateDate { get; set; }

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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public string FileUploadName { get; set; }
        public string Version { get; set; }

        public int ArticleCategoryId { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
         
        public int ArticleTypeId { get; set; }
        public virtual ArticleType ArticleType { get; set; }
                 
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }


        public Article()
        {            
            this.UploadedFilesKB = new HashSet<UploadedFileKB>();
        }

        public virtual ICollection<UploadedFileKB> UploadedFilesKB { get; set; } 
        
        
    }
}