using OilStationMVC.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    public class ChatController : ApiController
    {
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SendNotification(NotifModels obj)
        {
            NotificationHub objNotifHub = new NotificationHub();
            Notification objNotif = new Notification();
            objNotif.SentTo = obj.UserID;

            //context.Configuration.ProxyCreationEnabled = false;
            //context.Notifications.Add(objNotif);
            //context.SaveChanges();

            objNotifHub.SendNotification(objNotif.SentTo);

            //var query = (from t in context.Notifications  
            //             select t).ToList();  

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class NotifModels
    {
        public string UserID { get;  set; }
        public string Message { get;  set; }

    }

    internal class Notification
    {
        public string SentTo { get; internal set; }
    }
}