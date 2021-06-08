using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OpenPop.Mime;
using OpenPop.Pop3;
//using Prabhu;
using System.Threading.Tasks;
using OilStationMVC.ViewModels;
using OilStationMVC.AuthenticationAndAuthorization;
using System.Data;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class EmailController : Controller
    {
        public ActionResult Inbox()
        {
            //List<EmailViewModel> lst = new List<EmailViewModel>()
            //{
            //    new EmailViewModel
            //    {
            //        Id=1,From="mahmoud_saleh@yahoo.com",To="a@a.com", Subject="Subject 1",Body="Body 1",IsSelected=false
            //    },
            //    new EmailViewModel
            //    {
            //        Id=2,From="mahmoud_saleh@yahoo.com",To="a@a.com", Subject="Subject 1",Body="Body 1",IsSelected=false
            //    },
            //      new EmailViewModel
            //    {
            //        Id=3,From="mahmoud_saleh@yahoo.com",To="a@a.com", Subject="Subject 1",Body="Body 1",IsSelected=false
            //    },
            //      new EmailViewModel
            //    {
            //        Id=4,From="mahmoud_saleh@yahoo.com",To="a@a.com", Subject="Subject 1",Body="Body 1",IsSelected=false
            //    },
            //      new EmailViewModel
            //    {
            //        Id=5,From="mahmoud_saleh@yahoo.com",To="a@a.com", Subject="Subject 1",Body="Body 1",IsSelected=false
            //    },
            //      new EmailViewModel
            //    {
            //        Id=6,From="mahmoud_saleh@yahoo.com",To="a@a.com", Subject="Subject 1",Body="Body 1",IsSelected=false
            //    }

            //};
            List<EmailViewModel> lst=  ReadEmailsFromId();
            return View(lst);
        }
        public List<EmailViewModel> ReadEmailsFromId()
        {
            List<EmailViewModel> lst = new List<EmailViewModel>();
            try
            {
                using (Pop3Client client = new Pop3Client())
                {
                    client.Connect("pop.mail.yahoo.com", 995, true); //For SSL                
                    client.Authenticate("mahmoud_saleh92@yahoo.com", "ms230289m");

                    for (int i = client.GetMessageCount() - 1; i >= 0; i--)
                    {
                        var msg = client.GetMessage(i);
                        MailMessage mail = msg.ToMailMessage();
                        lst.Add(new EmailViewModel
                        {
                            From = mail.From,
                            Body = mail.Body,
                            Subject = mail.Subject,
                            To = mail.To,
                            Attach = mail.Attachments
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                return lst;
            }
            return lst;
        }
        public ActionResult Read(string Id)
        {
            EmailViewModel model = new EmailViewModel
            {
                //Id = 1,
                //From = "mahmoud_saleh@yahoo.com",
                //To = "a@a.com",
                Subject = "Subject 1",
                Body = "Body 1",
                IsSelected = false
            };
            return View(model);
        }
        public ActionResult Compose(string Id)
        {

            return View();
        } 
    } 
} 