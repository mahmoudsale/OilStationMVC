using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class CheckBox
    {
        public TagBuilder tb = new TagBuilder("input");
        StringBuilder builder = new StringBuilder();
        public CheckBox()
        {
            tb.Attributes.Add("type", "checkbox");
            tb.Attributes.Add("class", "form-control");
        }

        public string txtId { get; set; }
        public string txtName { get; set; }
        public CheckBox ID(string Id)
        {
            txtId = Id;
            tb.Attributes.Add("id", txtId);
            return this;
        }
        public CheckBox Name(string Name)
        {
            txtName = Name;
            tb.Attributes.Add("name", txtName);
            return this;
        }
        public CheckBox Value(bool value)
        {
            if (value)
            {
                tb.Attributes.Add("value", "true");
                tb.Attributes.Add("checked", null);
            }
            return this;
        }

        public CheckBox ReadOnly(bool value)
        {
            if (value)
            {
                tb.Attributes.Add("readonly", "readonly");
            }
            return this;
        }

        public CheckBox CssClass(string CssClass)
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

        public IHtmlString RenderAsSwitchMode()
        {
            builder.Append("<div class='form-check form-check-switch form-check -switch-left'><label class='form-check-label d-flex align-items-center'> ");

            tb.Attributes.Add("data-on-color", "success");
            tb.Attributes.Add("data-off-color", "danger");
            tb.Attributes.Add("data-on-text", "مفعلة");
            tb.Attributes.Add("data-off-text", "غير مفعلة");
            tb.Attributes.Add("data-size", "small");
            tb.Attributes.Add("class", "form-check-input-switch");
            builder.Append(tb.ToString(TagRenderMode.SelfClosing));
            builder.Append("</label></div>");
            MvcHtmlString mvcHtmlString = new MvcHtmlString(builder.ToString());
            return mvcHtmlString;

        }
        public IHtmlString Render()
        {

            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
            return mvcHtmlString;
        }
    }
}