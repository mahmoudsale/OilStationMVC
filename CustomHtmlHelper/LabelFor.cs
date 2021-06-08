using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public class LabelFor : IControls
    {
        public TagBuilder tb = new TagBuilder("label");
        public LabelFor()
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

        public IControls CssClass(string CssClass)
        {
            if (!string.IsNullOrEmpty(CssClass))
            {
                tb.Attributes.Add("class", "form-control");
            }
            return this;
        }

        public LabelFor SetModelProperty<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string propertyName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!tb.Attributes.ContainsKey("id"))
            {
                tb.Attributes.Add("id","lbl"+ propertyName);
            }
            if (!tb.Attributes.ContainsKey("name"))
            {
                tb.Attributes.Add("name", "lbl" + propertyName);
            }
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                //return MvcHtmlString.Empty;
                return this;
            }
            tb.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            TagBuilder span = new TagBuilder("span");
            span.SetInnerText(labelText);

            tb.InnerHtml = span.ToString(TagRenderMode.Normal);
            return this;
        }

        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }
    }
}