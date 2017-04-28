using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  //Need this else the HtmlHelper method won't work and it won't appear in the razor view despite having the using statement

//using System.Web.WebPages.Html;


namespace ITAssetsDatabase.HtmlHelpers
{
    public static class HtmlHelpers
    {

        public static IHtmlString DisplayFormattedData(this HtmlHelper htmlHelper, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            string myString = data;
            myString = myString.Replace("\n", "<br>");

            return new HtmlString(myString);
        }
    }


    public static class MyExtensionMethods
    {
        public static IHtmlString LabelWithMark(this HtmlHelper helper, string content)
        {
            string htmlString = String.Format("<label><mark>{0}</mark></label>", content);
            return new HtmlString(htmlString);
        }
    }






}