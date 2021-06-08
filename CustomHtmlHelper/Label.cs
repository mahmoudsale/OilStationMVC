using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class Label : IControls
    {
        public TagBuilder tb = new TagBuilder("label");
        public Label()
        {

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
            return this;
        }

        public IControls ReadOnly(bool value)
        {
            return this;
        }
        public IControls Value(string value)
        {
            tb.SetInnerText(value);
            return this;
        }
        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }

        public IControls CssClass(string CssClass)
        {
            return this;
        }
    }
}