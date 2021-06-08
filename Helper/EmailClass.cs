using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace OilStationMVC.Helper
{
    public static class EmailClass
    {
        public static void SendEmailMessage(string Subject, string Body, string Email)
        {
            using (var smtp = new SmtpClient())
            {
                var GoogleCredintial = new NetworkCredential
                {
                    UserName = "mkhalifa230289@gmail.com",
                    Password = "ms230289m"
                };
                smtp.Credentials = GoogleCredintial;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                MailMessage message = new MailMessage();
                message.To.Add(Email);
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = true;
                message.From = new MailAddress("mkhalifa@link.sa");
                smtp.Send(message);

            }
        }
    }
}