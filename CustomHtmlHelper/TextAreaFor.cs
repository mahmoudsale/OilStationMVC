using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class TextAreaFor
    {
        public TagBuilder tb = new TagBuilder("textarea");
        public TextAreaFor()
        {
            tb.Attributes.Add("class", "form-control");
            //tb.Attributes.Add("type", "text");
            tb.Attributes.Add("rows", "5");
        }

        public TextAreaFor SetModelProperty<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
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
        public TextAreaFor ReadOnly(bool value)
        {
            if (value)
            {
                tb.Attributes.Add("readonly", "readonly");
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