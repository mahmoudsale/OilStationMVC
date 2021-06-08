using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OilStationMVC.CommonFolder
{


       
    public class CallActionLogFiles : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string Message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " -> " + "\t" + filterContext.ActionDescriptor.ActionName;
            WriteToLogFiles(Message);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string Message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " -> " + "\t" + filterContext.ActionDescriptor.ActionName;
            WriteToLogFiles(Message);

        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string Message = "\n" + filterContext.RouteData.Values["controller"] + " -> " + "\t" + filterContext.RouteData.Values["action"];
            WriteToLogFiles(Message);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string Message = "\n" + filterContext.RouteData.Values["controller"] + " -> " + "\t" + filterContext.RouteData.Values["action"];
            WriteToLogFiles(Message);
        }
        public void OnException(ExceptionContext filterContext)
        {
            string Message = "\n" + filterContext.RouteData.Values["controller"] + " -> " + "\t" + filterContext.RouteData.Values["action"] + "\n -> " + "\t" +filterContext.Exception.Message;
            WriteToLogFiles(Message);

            //if (!filterContext.ExceptionHandled)
            //{ 
            //    filterContext.Result = new RedirectResult("~/Error/Exception");
            //    filterContext.ExceptionHandled = true;
            //}

            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];

            Exception custException = new Exception("There is some error"); 
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName); 
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
               
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            }; 
            filterContext.ExceptionHandled = true;

            if (filterContext.Exception is HttpException)
            {
                var statusCode = ((HttpException)filterContext.Exception).GetHttpCode();
            }
        }
        public void WriteToLogFiles(string Data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Data/Data.txt"), Data);
        }
    }
}