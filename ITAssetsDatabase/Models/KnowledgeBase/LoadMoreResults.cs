using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models.KnowledgeBase
{
    public class LoadMoreResults
    {
        public int ArticleTypeId { get; set; }  
        public string SearchString { get; set; }
        public int PagesizeCount { get; set; }
        public int DisplayedResultsCount { get; set; }
        public int RemainingResultsCount { get; set; }
        public int MyProperty { get; set; }
         
    }
}