using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class TextArea
    {
        public TagBuilder tb = new TagBuilder("textarea");

        public TextArea()
        {
            //tb.Attributes.Add("type", "text");
            tb.Attributes.Add("class", "form-control p-20 border-0 rounded-0");
        }

        public string txtId { get; set; }
        public string txtName { get; set; }
        public TextArea ID(string Id)
        {
            txtId = Id;
            tb.Attributes.Add("id", txtId);
            return this;
        }
        public TextArea Name(string Name)
        {
            txtName = Name;
            tb.Attributes.Add("name", txtName);
            return this;
        }
        public TextArea Value(string value)
        {
            tb.Attributes.Add("value", value);
            return this;
        }
        public TextArea ReadOnly(bool value)
        {
            return this;
        }
        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }
    }
}