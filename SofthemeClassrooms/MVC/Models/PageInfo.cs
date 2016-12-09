using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalNumOfItems { get; set; }
        public int TotalPages { get { return TotalNumOfItems / ItemsPerPage; } }
    }
}