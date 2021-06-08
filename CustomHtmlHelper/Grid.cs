using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CustomHtmlHelper
{
    public static class Grid
    {
        public static List<Column> columnNameAndCaption = new List<Column>();
        public static string ClassName { get; set; }
        public static bool AllowEdit { get; set; }
        public static bool AllowDelete { get; set; }
        public static string KeyColumn { get; set; }
        public static object routeValues { get; set; }
        public static bool ShowEditAsPopup { get; set; }

        public static string Controller { get; set; }
        public static string ActionEdit { get; set; }
        public static string ActionDelete { get; set; }
        
        public static string ActionEditPopup { get; set; }
        public static IHtmlString MyGrid(this HtmlHelper helper, string name, IEnumerable list)
        {
            TagBuilder tb = new TagBuilder("table");
            tb.Attributes.Add("id", name);
            tb.Attributes.Add("name", name);
            tb.Attributes.Add("class", "table datatable table-striped table-hover");

            TagBuilder tbHeader = new TagBuilder("thead");
            tbHeader.Attributes.Add("id", "TableHeader");
        
            
              TagBuilder tbBody = new TagBuilder("tbody");
            tbBody.Attributes.Add("id", "TableBody");
            TagBuilder theaderRow = new TagBuilder("tr");

            if (columnNameAndCaption != null && columnNameAndCaption.Count > 0)
            {
                foreach (Column item in columnNameAndCaption)
                {
                    if (!string.IsNullOrEmpty(item.ColumnCaption) && !string.IsNullOrEmpty(item.ColumnName))
                    {
                        TagBuilder theaderCell = new TagBuilder("th");
                        theaderCell.SetInnerText(item.ColumnCaption.ToString());
                        theaderRow.InnerHtml += theaderCell.ToString();
                    }
                }
                if (AllowEdit)
                {
                    if (!string.IsNullOrEmpty(KeyColumn))
                    {
                        TagBuilder theaderCellEdit = new TagBuilder("th");
                        theaderCellEdit.SetInnerText("Edit");
                        theaderRow.InnerHtml += theaderCellEdit.ToString();
                    }
                }
                if (AllowDelete)
                {
                    if (!string.IsNullOrEmpty(KeyColumn))
                    {
                        TagBuilder theaderCellDelete = new TagBuilder("th");
                        theaderCellDelete.SetInnerText("Delete");
                        theaderRow.InnerHtml += theaderCellDelete.ToString();
                    }
                }
            }
            if (list != null)
            {
                foreach (var item in list)
                {
                    TagBuilder tbtr = new TagBuilder("tr");
                    foreach (Column PropName in columnNameAndCaption)
                    {
                        if (!string.IsNullOrEmpty(PropName.ColumnCaption) && !string.IsNullOrEmpty(PropName.ColumnName))
                        {
                            string PropValue = item.GetPropertyValue(PropName.ColumnName.ToString()).ToString();
                            TagBuilder tbCell = new TagBuilder("td");
                            tbCell.SetInnerText(PropValue);
                            tbtr.InnerHtml += tbCell;
                        }
                    }
                    if (AllowEdit)
                    {
                        if (!string.IsNullOrEmpty(KeyColumn))
                        {
                            TagBuilder tbCell = new TagBuilder("td");
                            TagBuilder tbLink = new TagBuilder("a");
                            tbLink.Attributes.Add("id", "Edit_" + item.GetPropertyValue(KeyColumn).ToString());
                            tbLink.SetInnerText("Edit");
                            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                            if (ShowEditAsPopup)
                            {
                                //tbLink.Attributes["href"] = urlHelper.Action("Edit", Controller, routeValues);
                                tbLink.Attributes["href"] = "#";
                                tbLink.Attributes.Add("data-toggle", "modal");
                                tbLink.Attributes.Add("class", "btn bg-success");
                                tbLink.Attributes.Add("data-target", "#exampleModal");
                                tbLink.Attributes.Add("onclick", "ShowEditPopup('"+ ActionEditPopup + "', '"+Controller+"'," + item.GetPropertyValue(KeyColumn).ToString() + ");");
                                tbCell.InnerHtml += tbLink;
                                tbtr.InnerHtml += tbCell;
                            }
                            else
                            {
                                tbLink.Attributes["href"] = urlHelper.Action(ActionEdit, Controller,  new { Id = item.GetPropertyValue(KeyColumn) });
               
                                tbCell.InnerHtml += tbLink;
                                tbtr.InnerHtml += tbCell;
                            }

                        }
                    }
                    if (AllowDelete)
                    {
                        if (!string.IsNullOrEmpty(KeyColumn))
                        {
                            TagBuilder tbCell = new TagBuilder("td");
                            TagBuilder tbLink = new TagBuilder("a");
                            tbLink.Attributes.Add("class", "btn bg-danger");
                            tbLink.Attributes.Add("id", "Del" + item.GetPropertyValue(KeyColumn).ToString());
                            tbLink.SetInnerText("Delete");
                            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                            tbLink.Attributes["href"] = urlHelper.Action(ActionDelete, Controller,  new { Id = item.GetPropertyValue(KeyColumn) });
                            tbCell.InnerHtml += tbLink;
                            tbtr.InnerHtml += tbCell;
                        }
                    }

                    tbBody.InnerHtml += tbtr;
                }
            }
            tbHeader.InnerHtml += theaderRow.ToString();
            tb.InnerHtml += tbHeader.ToString();
            tb.InnerHtml += tbBody.ToString();

            MvcHtmlString mvcHtmlString = new MvcHtmlString(tb.ToString());
            return mvcHtmlString;
        }
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            Type myType = obj.GetType();
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                return "";
            }
            //throw new ArgumentNullException(String.Format("{0} is invalid", propertyName));
            return property.GetValue(obj);
        }



        public static string Id(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
                return (string)routeValues["id"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                return HttpContext.Current.Request.QueryString["id"];

            return string.Empty;
        }

      
    }
}