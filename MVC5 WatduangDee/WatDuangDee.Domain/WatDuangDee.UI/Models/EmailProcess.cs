using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web; 

namespace WatDuangDee.UI.Models
{
    public class LeaveForComment
    {
        public string Topic { get; set; }
        public string Message { get; set; }
    }
    public class EmailModel
    {
      
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class EmailProcess
    {
        public virtual EmailModel emailSetting {get; set;}
        public EmailProcess()
        {
            emailSetting = new EmailModel();
        }
        public void EmailSendToAdmin()
        {
            
            MailMessage mail = new MailMessage();
            //foreach (var item in user) { 
            mail.To.Add("se552115077@vr.camt.info");
            //}

            mail.From = new MailAddress("se552115077@vr.camt.info");
            mail.Subject = emailSetting.Subject;
            string Body = emailSetting.Body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("se552115077@vr.camt.info", "552115077");// Enter seders User name and password
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}