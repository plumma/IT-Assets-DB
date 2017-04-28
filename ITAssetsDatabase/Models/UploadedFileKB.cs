using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class UploadedFileKB
    {
        public int id { get; set; }
        public string FileName { get; set; }

        
        public int ArticleId { get; set; }         
        public virtual Article Article { get; set; }
         
        
         
    } 
}    