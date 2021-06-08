using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class DateFor : IControls
    {
        public TagBuilder tb = new TagBuilder("input");
        public DateFor()
        {
            tb.Attributes.Add("type", "date");
            tb.Attributes.Add("class", "form-control");
        }
        public IControls CssClass(string CssClass)
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
        public DateFor SetModelProperty<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
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
            //if (metadata.Model != null)
            //{
            //    tb.Attributes.Add("value", metadata.Model.ToString());
            //}
            //textBox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            //return MvcHtmlString.Create(textBox.ToString(TagRenderMode.Normal));
            return this;
        }
    }
}