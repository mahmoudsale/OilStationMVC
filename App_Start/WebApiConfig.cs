using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace OilStationMVC.App_Start
{
    public static class WebApiConfig
    {
        public class CustomMediaTypeFormatter:JsonMediaTypeFormatter
        {
            public CustomMediaTypeFormatter()
            {
                this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            }
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.Remove(config.Formatters.XmlFormatter);//only REturn Json Format
            //config.Formatters.Remove(config.Formatters.JsonFormatter);//only REturn xml Format

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));//return json instead of xml
            config.Formatters.Add(new CustomMediaTypeFormatter());
        }
    }
}