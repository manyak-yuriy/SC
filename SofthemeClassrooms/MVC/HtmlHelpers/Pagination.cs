using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.HtmlHelpers
{
    public static class Pagination
    {
        public static MvcHtmlString PageLinks(PageInfo pInfo, string url)
        {
            StringBuilder result = new StringBuilder();

            return MvcHtmlString.Create(result.ToString());
        }
    }
}