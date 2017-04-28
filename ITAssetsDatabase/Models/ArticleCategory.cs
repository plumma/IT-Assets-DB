using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class ArticleCategory
    {
        public int Id { get; set; }

        public string Category { get; set; }


        
        public ArticleCategory()
        {            
            this.Articles = new HashSet<Article>();
        }
          
        public virtual ICollection<Article> Articles { get; set; } 
        
    

    }
}