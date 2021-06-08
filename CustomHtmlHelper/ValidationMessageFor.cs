using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;


using System.Web.Mvc.Html;

namespace OilStationMVC.CustomHtmlHelper
{
    public static class mValidationMessageFor
    {

        public static MvcHtmlString MyValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            TagBuilder containerDivBuilder = new TagBuilder("div");
            containerDivBuilder.AddCssClass("field-error-box");

            TagBuilder topDivBuilder = new TagBuilder("div");
            topDivBuilder.AddCssClass("top");

            TagBuilder midDivBuilder = new TagBuilder("div");
            midDivBuilder.AddCssClass("mid");
            midDivBuilder.InnerHtml = helper.ValidationMessageFor(expression).ToString();

            containerDivBuilder.InnerHtml += topDivBuilder.ToString(TagRenderMode.Normal);
            containerDivBuilder.InnerHtml += midDivBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(containerDivBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString DivValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(htmlHelper.ValidationMessageFor(expression).ToString().Replace("span", "div"));
        }

    }
}