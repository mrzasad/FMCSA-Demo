using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailServiceProvider
    {

        public async Task<bool> SendOTPTOEmail(string email, string emailOTP)
        {
            try
            {

                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string emailFromAddress = "noreplyFMCDemo@gmail.com";
                string password = "Matloob@123";

                MailMessage message = new MailMessage();
                message.From = new MailAddress("noreply@FMCDemo.app");
                message.To.Add(new MailAddress(email));
                message.Subject = "OTP Matloob";
                message.IsBodyHtml = true; //to make message body as html  

                string body = "<div style='line-height:inherit;font-family:Avenir,Helvetica,sans-serif;box-sizing:border-box;direction:ltr;text-align:left;'>";
                body += "<br style='line-height:inherit;'>Confirmation code: " + emailOTP;
                body += "<br style='line-height: inherit;'>";
                body += "This code expires in 10 minutes. <br style='line-height: inherit;'><br style='line-height: inherit;'>";
                body += "Thank you for using Matloob<br style='line-height: inherit;'>";
                body += "Matloob App <br style = 'line-height: inherit;' >";
                body += "Note: This is an electronic message.Please do not reply to this email.</div>";

                message.Body = "<p>" + body + "</p>";

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    //smtp.EnableSsl = false;

                    //smtp.Host = "smtp.gmail.com";//"smtpout.secureserver.net";
                    //smtp.Port = 587;
                    //smtp.Credentials = new NetworkCredential("noreply@FMCDemo.app", "12345678");
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {

                var msg = ex.Message;
                return false;
            }
        }

        public async Task<bool> SendPasswordTOEmail(string email, string emailPass)
        {
            try
            {

                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string emailFromAddress = "noreplyFMCDemo@gmail.com";
                string password = "Matloob@123";

                MailMessage message = new MailMessage();
                message.From = new MailAddress("noreply@FMCDemo.app");
                message.To.Add(new MailAddress(email));
                message.Subject = "OTP Matloob";
                message.IsBodyHtml = true; //to make message body as html  

                string body = "<div style='line-height:inherit;font-family:Avenir,Helvetica,sans-serif;box-sizing:border-box;direction:ltr;text-align:left;'>";
                body += "<br style='line-height:inherit;'>Account Password: " + emailPass;
                body += "<br style='line-height: inherit;'>";
                body += "Use this recovered password for login. <br style='line-height: inherit;'><br style='line-height: inherit;'>";
                body += "Thank you for using Matloob<br style='line-height: inherit;'>";
                body += "Matloob App <br style = 'line-height: inherit;' >";
                body += "Note: This is an electronic message.Please do not reply to this email.</div>";

                message.Body = "<p>" + body + "</p>";

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    //smtp.EnableSsl = false;

                    //smtp.Host = "smtp.gmail.com";//"smtpout.secureserver.net";
                    //smtp.Port = 587;
                    //smtp.Credentials = new NetworkCredential("noreply@FMCDemo.app", "12345678");
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {

                var msg = ex.Message;
                return false;
            }
        }
    }
}
