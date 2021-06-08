using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace OilStationMVC.Models
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }
     
        //public Email(string To, string From, string Subject, string Body, string Host, string senderEmail, string SenderEmailPassword)
        //{
        //    this.To = To;
        //    this.From = From;
        //    this.Subject = Subject;
        //    this.Body = Body;

        //    this.Host = Host;
        //    this.SenderEmial = senderEmail;
        //    this.SenderEmailPassword = SenderEmailPassword;
        //}
        //public void SendMail()
        //{
        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //    mail.To.Add(To);
        //    mail.From = new MailAddress(From, "Email head", System.Text.Encoding.UTF8);
        //    mail.Subject = Subject;
        //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //    mail.Body = Body;
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    mail.IsBodyHtml = true;
        //    mail.Priority = MailPriority.High;
        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential(SenderEmial, SenderEmailPassword);
        //    client.Port = 587;
        //    client.Host = Host;
        //    client.EnableSsl = true;
        //    try
        //    {
        //        client.Send(mail);
        //        //  Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception ex2 = ex;
        //        string errorMessage = string.Empty;
        //        while (ex2 != null)
        //        {
        //            errorMessage += ex2.ToString();
        //            ex2 = ex2.InnerException;
        //        }
        //        //  Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
        //    }
        //}
    }
}