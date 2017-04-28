using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.HtmlHelpers
{
    public static class StringExtensions
    {

        public static string SubStringTo(this string thatString, int limit)
        {
            if (!String.IsNullOrEmpty(thatString))
            {
                if (thatString.Length > limit)
                {
                    return ((thatString.Substring(0, limit)).Trim() + "...");
                }
                return thatString;
            }
            return string.Empty;
        }
     }
}