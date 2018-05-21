using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ApiService.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            MailMessage messageSmtp = new MailMessage();
            
            string fromEmail = "baracdany@gmail.com";
            string password = "dinamo100";
            string toEmail = message.Destination;
            messageSmtp.From = new MailAddress(fromEmail);
            messageSmtp.To.Add(toEmail);
            messageSmtp.Subject = message.Subject;
            messageSmtp.Body = message.Body;
            messageSmtp.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, password);

                smtpClient.Send(messageSmtp.From.ToString(), messageSmtp.To.ToString(), messageSmtp.Subject, messageSmtp.Body);
            }

            await Task.FromResult(0);

            //var myMessage = new SendGridMessage();

            //myMessage.AddTo(message.Destination);
            //myMessage.From = new System.Net.Mail.MailAddress("barac_dany@yahoo.com", "My awesome admin");
            //myMessage.Subject = message.Subject;
            //myMessage.Text = message.Body;
            //myMessage.Html = message.Body;

            //var credentials = new NetworkCredential("barucu", 
            //                                        "Dinamo100");

            //// Create a Web transport for sending email.
            //var transportWeb = new Web(credentials);

            //// Send the email.
            //if (transportWeb != null)
            //{
            //    await transportWeb.DeliverAsync(myMessage);
            //}
            //else
            //{
            //    //Trace.TraceError("Failed to create Web transport.");
               
            //}
        }
    }
}