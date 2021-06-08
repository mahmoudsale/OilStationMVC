using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace OilStationMVC.CustomHtmlHelper
{
    public static class CustomHtmlHelper
    {
        public static Image mImage(this HtmlHelper helper)
        {
            return new Image();
        }

        public static TextBox mTextBox(this HtmlHelper htmlHelper)
        {
            return new TextBox();
        }
        public static TextBoxFor mTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper,Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return new TextBoxFor().SetModelProperty(helper,expression);
        }
        public static TextArea mTextArea(this HtmlHelper htmlHelper)
        {
            return new TextArea();
        }
        public static TextAreaFor mTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return new TextAreaFor().SetModelProperty(helper, expression);
        }
        public static DropdownList mDropdownList(this HtmlHelper htmlHelper)
        {
            return new DropdownList();
        }

        public static DropdownListFor mDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return new DropdownListFor().SetModelProperty(helper, expression);
        }

        public static CheckBox mCheckBox(this HtmlHelper htmlHelper)
        {
            return new CheckBox();
        }
        public static CheckBoxFor mCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return new CheckBoxFor().SetModelProperty(helper, expression);
        }
        public static Button mButton(this HtmlHelper htmlHelper)
        {
            return new Button();
        }
        public static Label mLabel(this HtmlHelper htmlHelper)
        {
            return new Label();
        }

        public static LabelFor mLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return new LabelFor().SetModelProperty(helper, expression);
        }

        public static LoadPanel mLoadPanel(this HtmlHelper htmlHelper)
        {
            return new LoadPanel();
        }

        public static Date mDate(this HtmlHelper htmlHelper)
        {
            return new Date();
        }
        public static DateFor mDateFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return new DateFor().SetModelProperty(helper, expression);
        }
        public static MvcHtmlString Custom_TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
       
            return Custom_TextBoxFor(helper, expression, null);
        }

        //This overload accepts expression and htmlAttributes object as parameter.
        public static MvcHtmlString Custom_TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            //Fetching the metadata related to expression. This includes name of the property, model value of the property as well.
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            //Fetching the property name.
            string propertyName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            //Creating a input tag using TagBuilder class.
            TagBuilder textBox = new TagBuilder("input");
            //Setting the tags property type to text to render textbox.
            textBox.Attributes.Add("type", "text");
            //Setting the name and id attribute.
            textBox.Attributes.Add("name", propertyName);
            textBox.Attributes.Add("id", propertyName);

            //Setting the value attribute of textbox with model value if present.
            if (metadata.Model != null)
            {
                textBox.Attributes.Add("value", metadata.Model.ToString());
            }
            textBox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(textBox.ToString(TagRenderMode.Normal));
        }

    }
}