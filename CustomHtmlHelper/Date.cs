using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class Date : IControls
    {
        public TagBuilder tb = new TagBuilder("input");
        public Date()
        {
            tb.Attributes.Add("type", "date");

        }
        public IControls CssClass(string CssClass)
        {
            if (!string.IsNullOrEmpty(CssClass))
            {
                tb.Attributes.Add("class", CssClass);
            }
            return this;
        }

        public IControls ID(string Id)
        {
            tb.Attributes.Add("id", Id);
            return this;
        }

        public IControls Name(string Name)
        {
            tb.Attributes.Add("name", Name);
            return this;
        }

        public IControls PlaceHolder(string PlaceHolder)
        {
            tb.Attributes.Add("placeholder", PlaceHolder);
            return this;
        }

        public IControls ReadOnly(bool value)
        {
            if (value)
            {
                tb.Attributes.Add("readonly", "readonly");
            }
            return this;
        }

        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
            return mvcHtmlString;
        }

        public IControls Value(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                tb.Attributes.Add("value", value);
            }
            return this;
        }
    }
}