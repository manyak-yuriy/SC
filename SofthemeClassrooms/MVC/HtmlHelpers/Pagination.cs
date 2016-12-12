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
        public static MvcHtmlString PageLinks(this HtmlHelper helper,PageInfo pInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pInfo.TotalNumOfItems < pInfo.ItemsPerPage)
            {
                return MvcHtmlString.Empty;
            }

            int pageNumber = pInfo.PageNumber,
                totalPages = pInfo.TotalPages;

            if (totalPages < 10)
            {
                for (int i = 1; i <= totalPages; ++i)
                {
                    var link = MakeLink(i, pInfo, pageUrl);
                    result.Append(link);
                }
            }
            else if (pageNumber <= 2)
            {
                for (int i = 1; i <= 3; ++i)
                {
                    var link = MakeLink(i, pInfo, pageUrl);
                    result.Append(link);
                }
                result.Append("...");
                int mid = totalPages / 2;
                result.Append(MakeLink(mid, pInfo, pageUrl));
                result.Append("...");
                result.Append(MakeLink(totalPages, pInfo, pageUrl));
            }
            else if (pageNumber >= totalPages - 2)
            {
                result.Append(MakeLink(1, pInfo, pageUrl));
                result.Append("...");
                result.Append(MakeLink(totalPages/2, pInfo, pageUrl));
                result.Append("...");
                for (int i = totalPages - 3; i <= totalPages; ++i)
                {
                    var link = MakeLink(i, pInfo, pageUrl);
                    result.Append(link);
                }
            }
            else
            {
                result.Append(MakeLink(1, pInfo, pageUrl));
                result.Append("...");
                for (int i = pageNumber - 1; i <= pageNumber + 1; ++i)
                {
                    var link = MakeLink(i, pInfo, pageUrl);
                    result.Append(link);
                }
                result.Append("...");
                result.Append(MakeLink(totalPages, pInfo, pageUrl));
            }

            return MvcHtmlString.Create(result.ToString());
        }


        public static string MakeLink(int num, PageInfo pInfo, Func<int, string> pageUrl)
        {
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrl(num));
            a.InnerHtml = num.ToString();

            if (num == pInfo.PageNumber)
            {
                a.AddCssClass("link-active");
            }
            a.AddCssClass("buttonLink");

            return a.ToString();
        }
    }
}