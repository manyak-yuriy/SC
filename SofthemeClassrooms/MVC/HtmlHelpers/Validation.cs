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
            TagBuilder icon = new TagBuilder("i");
            icon.AddCssClass("fa");
            icon.AddCssClass("fa-info-circle");

            result.Append(icon.ToString(TagRenderMode.Normal));

            TagBuilder DivBuilder = new TagBuilder("div");
            DivBuilder.AddCssClass("mid");
            DivBuilder.InnerHtml = helper.ValidationMessageFor(expression).ToString();

            result.Append(DivBuilder.ToString(TagRenderMode.Normal));
            TagBuilder wrapper = new TagBuilder("div");
            wrapper.InnerHtml = result.ToString();
            wrapper.AddCssClass("red-error");

            return MvcHtmlString.Create(result.ToString());
        }
    }
}