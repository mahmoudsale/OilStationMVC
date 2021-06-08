using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace OilStationMVC.AuthenticationAndAuthorization
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    if (actionContext.Request.Headers.Authorization == null)
        //    {
        //        actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
        //    }
        //    else
        //    {
        //        string AuthenticationToken = actionContext.Request.Headers.Authorization.Parameter;
        //        string DecodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(AuthenticationToken));
        //        string[] UserNamePasswordArr = DecodedAuthenticationToken.Split(':');
        //        string UserName = UserNamePasswordArr[0];
        //        string Password = UserNamePasswordArr[1];
        //        if (UserSecurity.Login(UserName, Password))
        //        {
        //            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserName), null);
        //        }
        //        else
        //        {
        //            actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
        //        }
        //    }
        //}
    }

    public class mAuthorize : System.Web.Mvc.AuthorizeAttribute
    {

        public mAuthorize(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
        //public mAuthorize(params int[] roles)
        //{
        //    Roles = string.Join(",", roles);
        //}
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //string userGroupID = UserIdentity.UserGroupId;
            //string[] idRolesArray = this.Roles.Split(',');
            //if (!string.IsNullOrEmpty(userGroupID))
            //{
            //    if (idRolesArray.Contains(userGroupID))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}
            //else
            //{
            //    return true;
            //}
         return   base.AuthorizeCore(httpContext);

        }



        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {


            //if (filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    filterContext.Result = new HttpStatusCodeResult(403, "Forbidden");
            //}

            //else
            //{
            //    filterContext.HttpContext.Response.StatusCode = 403;

            //    filterContext.Result = new RedirectResult("~/Pages/accessdenied.html");
            //}

           
            // Returns HTTP 401 by default - see HttpUnauthorizedResult.cs.
            filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    { "action", "AccessDeinied" },
                    { "controller", "Account" },
                 
                        });
        }


        //public string[] Get(string controller, string action)
        //{
        //    // get your roles based on the controller and the action name 
        //    // wherever you want such as db
        //    // I hardcoded for the sake of simplicity 
        //    return new string[] { "Student", "Teacher" };
        //}

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var controller = httpContext.Request.RequestContext
        //        .RouteData.GetRequiredString("controller");
        //    var action = httpContext.Request.RequestContext
        //        .RouteData.GetRequiredString("action");
        //    // feed the roles here
        //    Roles = string.Join(",", Get(controller, action));
        //    return base.AuthorizeCore(httpContext);
        //}

    }
}