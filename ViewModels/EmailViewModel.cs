using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        public MailAddress From { get; set; }
        public MailAddressCollection To { get; set; }
        public AttachmentCollection Attach { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSelected { get; set; }

    }
}
