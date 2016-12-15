using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class ExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }


            filterContext.Result = new RedirectResult("/ErrorHandler/ErrorOccurred");
            filterContext.ExceptionHandled = true;
        }
    }
}