using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class TextBox
    {
        public TagBuilder tb = new TagBuilder("input");

        public TextBox()
        {
            tb.Attributes.Add("type", "text");
            tb.Attributes.Add("class", "form-control");
            tb.Attributes.Add("autocomplete", "off");
        }

        public string txtId { get; set; }
        public string txtName { get; set; }
        public TextBox ID(string Id)
        {
            txtId = Id;
            tb.Attributes.Add("id", txtId);
            return this;
        }
        public TextBox Name(string Name)
        {
            txtName = Name;
            tb.Attributes.Add("name", txtName);
            return this;
        }
        public TextBox Value(string value)
        {
            tb.Attributes.Add("value", value);
            return this;
        }
        public TextBox ReadOnly(bool value)
        {
            if(value)
            {
                tb.Attributes.Add("readonly", "readonly");
            }
            return this;
        }
        public TextBox AutoComplete(bool value)
        {
            if (tb.Attributes.ContainsKey("autocomplete"))
            {
                tb.Attributes.Remove("autocomplete");
            }
            if (!value)
            {
                tb.Attributes.Add("autocomplete", "off");
            }
            else
            {
                tb.Attributes.Add("autocomplete", "on");
            }
            return this;
        }
        public TextBox PlaceHolder(string PlaceHolder)
        {
            if (!string.IsNullOrEmpty(PlaceHolder))
            {
                tb.MergeAttribute("placeholder", PlaceHolder);
            }
            return this;
        }


        public TextBox  CssClass(string CssClass)
        {


            if (!string.IsNullOrEmpty(CssClass))
            {
                if (tb.Attributes.ContainsKey("class"))
                    tb.Attributes["class"] = tb.Attributes["class"] + " " + CssClass;
                else
                    tb.Attributes.Add("class", CssClass);

            }
            return this;
        }


        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
            return mvcHtmlString;
        }
    }


}