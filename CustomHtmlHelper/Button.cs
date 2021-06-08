using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class Button
    {
        public TagBuilder tb = new TagBuilder("button");

       

        public string txtId { get; set; }
        public string txtName { get; set; }
        public Button ID(string Id)
        {
            txtId = Id;
            tb.Attributes.Add("id", txtId);
            return this;
        }
        public Button Name(string Name)
        {
            txtName = Name;
            tb.Attributes.Add("name", txtName);
            return this;
        }
        public Button Value(string value)
        {
            tb.SetInnerText( value);
            return this;
        }
      
        public Button PlaceHolder(string PlaceHolder)
        {
            if (!string.IsNullOrEmpty(PlaceHolder))
            {
                tb.MergeAttribute("placeholder", PlaceHolder);
            }
            return this;
        }
        public Button CssClass(string CssClass)
        {
            if (!string.IsNullOrEmpty(CssClass))
            {
                tb.MergeAttribute("class", CssClass);
            }
            return this;
        }
        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }
    }
}