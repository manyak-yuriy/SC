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
        public static MvcHtmlString PageLinks(PageInfo pInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pInfo.TotalNumOfItems > pInfo.ItemsPerPage)
            {
                return MvcHtmlString.Empty;
            }

            int pageNumber = pInfo.PageNumber;
            if(pageNumber > 1)
            {
                for(int i = 1; i <= 10; ++i)
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", pageUrl(i));
                    a.InnerHtml = i.ToString();

                    if(i == pInfo.PageNumber)
                    {
                        a.AddCssClass("btn btn-active");
                    }
                    else
                    {
                        a.AddCssClass("btn btn-default");
                    }
                    result.Append(a.ToString());
                }
            }         

            return MvcHtmlString.Create(result.ToString());
        }
    }
}