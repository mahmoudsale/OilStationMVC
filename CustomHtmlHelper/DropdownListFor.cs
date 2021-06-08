using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class DropdownListFor
    {
        public TagBuilder tb = new TagBuilder("select");
        public DropdownListFor()
        {
            tb.Attributes.Add("class", "form-control ");
    
        }

        public DropdownListFor SetModelProperty<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string propertyName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!tb.Attributes.ContainsKey("id"))
            {
                tb.Attributes.Add("id", propertyName);
            }
            if (!tb.Attributes.ContainsKey("name"))
            {
                tb.Attributes.Add("name", propertyName);
            }
            if (metadata.Model != null)
            {
                tb.Attributes.Add("value", metadata.Model.ToString());
            }
            //textBox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            //return MvcHtmlString.Create(textBox.ToString(TagRenderMode.Normal));
            return this;
        }
        public DropdownListFor ReadOnly(bool value)
        {
            if (value)
            {
                tb.Attributes.Add("readonly", "readonly");
            }
            return this;
        }

        public DropdownListFor DataSource(List<SelectListItem> lst)
        {
           
            if (lst.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var item in lst)
                {
                    builder.Append(new Image().ID("s").SRC(@"~\Image\1.png").Alt("s").Render()+ "<option value='" + item.Value + "'>" + item.Text + "</option>");
                }
                tb.InnerHtml += builder.ToString();
            }
            return this;
        }

        public DropdownListFor OnChange(string FuncName)
        {
            if(!string.IsNullOrEmpty(FuncName))
            {
                tb.Attributes.Add("onchange", FuncName);
            }
            return this;
        }
        public DropdownListFor EnabledSearch(bool value)
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

        public DropdownListFor CssClass(string CssClass)
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
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }
    }
}