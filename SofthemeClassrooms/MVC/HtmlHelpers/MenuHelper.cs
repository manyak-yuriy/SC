using System.Web;
using System.Web.Mvc;

namespace WebApplication1.HtmlHelpers
{
    public static class MenuHelper
    {

        public static MenuItemInfo[] menuItems;
        static MenuHelper()
        {
            UrlHelper url = new UrlHelper();
            
        }

        public enum MenuItems{ Login, Feedback, Classrooms };
        public static MvcHtmlString CreateMenuItems(this HtmlHelper h, MenuItemInfo[] menuItems,  int activeItem)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("menu-items");
            for(int i = 0; i < menuItems.Length; ++i)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.InnerHtml = menuItems[i].ItemName;
                a.AddCssClass("link");
                a.MergeAttribute("href", menuItems[i].ActionRoute);
                li.InnerHtml = a.ToString();
                li.AddCssClass("menu-item");
                if(i == activeItem)
                {
                    li.AddCssClass("menu-item-active");
                }
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }
    }
}