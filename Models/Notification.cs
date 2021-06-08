using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilStationMVC.Models
{
    public class Notification
    {
        public Notification()
        {
            BranchId = CommonFolder.Common.Current.BranchId;
            Type = "1";
            Details = "New Item";
            Title = "New Item";
            DetailsURL = "/home/Index";
            SentTo = "All";
            Date = DateTime.Now.ToString();
            IsRead = false;
            IsDeleted = false;
            IsReminder = false;
            Code = "1";
            NotificationType = "New item";
            SinceFor = "";
            IsNew = false;
        }
        public int BranchId { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
        public string DetailsURL { get; set; }
        public string SentTo { get; set; }
        public string Date { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReminder { get; set; }
        public string Code { get; set; }
        public string NotificationType { get; set; }
        public string SinceFor { get; internal set; }
        public bool IsNew { get;  set; }
    }
}