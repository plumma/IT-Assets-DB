using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models.KnowledgeBase
{
    public class AddArticleViewModel
    {
        [DataType(DataType.MultilineText)]
        [Display(Name= "Summary")]
        [Required(ErrorMessage = "The Summary field is required.")]
        [StringLength(200, ErrorMessage = "Summary cannot be longer than 200 characters.")]
        public string Summary { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Details")]
        [Required(ErrorMessage = "The Details field is required.")]
        public string Details { get; set; }

        [DataType(DataType.MultilineText)]
        public string Resolution { get; set; }

        public string FileUploadName { get; set; }

        public int ArticleCategoryId { get; set; }
        public SelectList ArticleCategoryList { get; set; }
         
        public int ArticleTypeId { get; set; }
        public SelectList ArticleTypeList { get; set; }

        public int ApplicationId { get; set; }
        public SelectList ApplicationList { get; set; }

        //[Display(Name ="Application Name")]
        //[Required(ErrorMessage = "Please lookup the relavant application.")]
        //public string ApplicationName { get; set; }  // Form validation only
         
        public string Version { get; set; }
    }
}  