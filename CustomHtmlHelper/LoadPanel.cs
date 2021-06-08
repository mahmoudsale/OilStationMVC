using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class LoadPanel
    {
        public TagBuilder tb = new TagBuilder("div");
   
        public LoadPanel ID(string Id)
        {
            tb.Attributes.Add("id", Id);

            return this;
        }
        public LoadPanel Name(string Name)
        {
            tb.Attributes.Add("name", Name);
            return this;
        }
        public LoadPanel CssClass(string CssClass)
        {
            tb.Attributes.Add("class", CssClass);
            return this;
        }
        public IHtmlString Render()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<div class='spinner-border role=status>");
            builder.Append("<span class='sr-only'>Loading...</span>");
            builder.Append("</div>");
            builder.Append("</div>");
            tb.InnerHtml = builder.ToString();
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }

        public LoadPanel ShadingColor(string rgbaColor)
        {
            return this;
        }
        public LoadPanel Position(string rgbaColor)
        {
            return this;
        }
        public LoadPanel Visible(bool value)
        {
            return this;
        }
        public LoadPanel ShowIndicator(bool value)
        {
            return this;
        }
        public LoadPanel ShowPane(bool value)
        {
            return this;
        }
        public LoadPanel Shading(bool value)
        {
            return this;
        }
        public LoadPanel CloseOnOutsideClick(bool value)
        {
            return this;
        }
        public LoadPanel OnShown(string Fun)
        {
            return this;
        }
        public LoadPanel OnHidden(string Fun)
        {
            return this;
        }
    }


}