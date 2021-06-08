using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OilStationMVC.CustomHtmlHelper
{
    public class TextBoxFor
    {

        public TagBuilder tb = new TagBuilder("input");
        public TextBoxFor()
        {
            tb.Attributes.Add("class", "form-control");
            tb.Attributes.Add("type", "text");
            tb.Attributes.Add("autocomplete", "off");
        }

        public TextBoxFor SetModelProperty<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
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
            bool IsRequired = metadata.IsRequired;
            if (IsRequired)
            {
                tb.Attributes.Add("required", "required");
            }
            return this;
        }



        public TextBoxFor Value(string value)
        {
            tb.Attributes.Add("value", value);
            return this;
        }


        public TextBoxFor ReadOnly(bool value)
        {
            if (value)
            {
                tb.Attributes.Add("readonly", "readonly");
            }
            return this;
        }
        public TextBoxFor AutoComplete(bool value)
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

        public TextBoxFor CssClass(string CssClass)
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
        public TextBoxFor PlaceHolder(string PlaceHolder)
        {
            if (!string.IsNullOrEmpty(PlaceHolder))
            {
                if (tb.Attributes.ContainsKey("placeholder"))
                    tb.Attributes["placeholder"] = tb.Attributes["placeholder"] + " " + PlaceHolder;
                else
                    tb.Attributes.Add("placeholder", PlaceHolder);

            }
            return this;
        }

        public TextBoxFor OnChange(string FuncName)
        {
            if (!string.IsNullOrEmpty(FuncName))
            {
                tb.Attributes.Add("onChange", FuncName);
            }
            return this;
        }

        public TextBoxFor ShowClearButton(bool Value)
        {
            if (Value)
            {
                StringBuilder builder = new StringBuilder();
            }
            return this;
        }

        //public TextBoxFor InputDataType(DataTypes dataTypes)
        //{
        //    switch (dataTypes)
        //    {
        //        case DataTypes.Integer:
        //            tb.Attributes.Add("onkeypress", "return isNumberKey(event)");
        //            break;
        //        case DataTypes.String:
        //            tb.Attributes.Add("onkeypress", "return isNumericKey(event)");
        //            break;
        //        case DataTypes.Decimal:
        //            tb.Attributes.Add("onkeypress", "return IsNumeric(event)");
        //            break;
        //        default:
        //            break;

        //    }
        //    return this;
        //}
        public TextBoxFor MaxLength(int Value)
        {
            tb.Attributes.Add("maxlength", Value.ToString());
            return this;
        }


        public IHtmlString Render()
        {
            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
            return mvcHtmlString;
        }
    }

    public static class x
    {
        public static MvcHtmlString ToolTipTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper,
    Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add("data-msg-number", "The field YearsInService must be a number.");
            attributes.Add("data-rule-number", true);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            bool IsRequired = metadata.IsRequired;
            if (IsRequired)
            {
                attributes.Add("data-rule-required", true);
            }
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(helper, expression, attributes);
        }
    }
}