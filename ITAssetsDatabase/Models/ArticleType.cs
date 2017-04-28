using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class ArticleType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArticleTypeImage { get; set; }

        public ArticleType()
        {            
            this.Articles = new HashSet<Article>();
        }
          
        public virtual ICollection<Article> Articles { get; set; } 
        

    } 
}     