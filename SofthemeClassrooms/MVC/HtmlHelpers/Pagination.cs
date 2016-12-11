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
            if(pInfo.TotalNumOfItems > pInfo.ItemsPerPage)
            {
                return MvcHtmlString.Empty;
            }

            int pageNumber = pInfo.PageNumber;
            if (pageNumber <= 10)
            {
                for(int i = 0; i < pageNumber; ++i)
                {

                }
            }            

            return MvcHtmlString.Create(result.ToString());
        }
    }
}