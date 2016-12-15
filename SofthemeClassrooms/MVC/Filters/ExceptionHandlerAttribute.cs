using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            /*
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("~/Views/Shared/Error.cshtml");
            */
        }
    }
}