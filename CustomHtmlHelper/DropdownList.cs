using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class DropdownList
    {
        public TagBuilder tb = new TagBuilder("select");
        public DropdownList()
        {
            tb.Attributes.Add("class", "form-control");
        }

        public string txtId { get; set; }
        public string txtName { get; set; }
        public DropdownList ID(string Id)
        {
            txtId = Id;
            tb.Attributes.Add("id", txtId);
            return this;
        }
        public DropdownList Name(string Name)
        {
            txtName = Name;
            tb.Attributes.Add("name", txtName);
            return this;
        }


        public DropdownList DataSource(List<SelectListItem> lst)
        {

            if (lst.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var item in lst)
                {
                    builder.Append("<option value='" + item.Value + "'>" + item.Text + "</option>");
                }
                tb.InnerHtml += builder.ToString();
            }
            return this;
        }


        public DropdownList EnabledSearch(bool value)
        {
            if (tb.Attributes.ContainsKey("class"))
            {
                if (value)
                {
                    tb.Attributes["class"] = tb.Attributes["class"] + " select-search";
                }
                else
                {
                    tb.Attributes["class"] = tb.Attributes["class"];

                }

            }
            else
            {
                if (value)
                {
                    tb.Attributes.Add("class", " select-search");
                }
                else
                {


                }
            }

            return this;
        }

        public DropdownList OnChange(string FuncName)
        {
            if (!string.IsNullOrEmpty(FuncName))
            {
                tb.Attributes.Add("onchange", FuncName);
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