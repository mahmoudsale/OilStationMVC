using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class Image
    {
        public TagBuilder tb = new TagBuilder("img");
        public Image ID(string Id)
        {
            tb.Attributes.Add("id", Id);
            return this;
        }
        public Image Name(string Name)
        {
            tb.Attributes.Add("name", Name);
            return this;
        }

        public Image SRC(string Source)
        {
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(Source));
            return this;
        }
        public Image Alt(string alt)
        {
            tb.Attributes.Add("alt", alt);
            return this;
        }

        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
            return mvcHtmlString;
        }

    }
}