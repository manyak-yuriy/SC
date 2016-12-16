using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebApplication1.HtmlHelpers
{
    public static class Validation
    {
        public static MvcHtmlString RedValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            StringBuilder result = new StringBuilder();
            //TagBuilder icon = new TagBuilder("i");
            //icon.AddCssClass("fa");
            //icon.AddCssClass("fa-info-circle");
            //HttpUtility.HtmlEncode("sda")
            var valMessage = helper.ValidationMessageFor(expression).ToString();

            TagBuilder DivBuilder = new TagBuilder("div");
            DivBuilder.AddCssClass("mid");
            DivBuilder.InnerHtml = valMessage;
            DivBuilder.AddCssClass("red-error");

            result.Append(DivBuilder.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(result.ToString());
        }
    }
}