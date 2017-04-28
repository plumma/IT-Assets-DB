using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class Application
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Application Name")]
        [Required(ErrorMessage = "An application name is required.")]
        [StringLength(200, ErrorMessage = "Application name cannot be longer than 200 characters.")]
        public string AppName { get; set; }
        

        [Display(Name = "Company")]
        [StringLength(200, ErrorMessage = "Company cannot be longer than 200 characters.")]
        public string Company { get; set; }


        public Application()
        {            
            this.Articles = new HashSet<Article>();
        }
          
        public virtual ICollection<Article> Articles { get; set; } 
        
    }
}