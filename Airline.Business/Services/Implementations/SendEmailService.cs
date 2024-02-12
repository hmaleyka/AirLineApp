using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public static class SendEmailService
    {
        public static void SendEmail(string to, string name)
        {
            if (name == "Airline Team")
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential("maleykaheybatova1011@gmail.com", "luzw ppnb motd bubl");
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage()
                    {
                        From = new MailAddress("maleykaheybatova1011@gmail.com"),
                        Subject = "Welcome to Airline App Website",
                        Body = $"Hello I am from {name}" +
                        $"<p>Welcome to AirLine App, Hello there!\r\n\r\nWe're thrilled to have you join us at Airline! You've just taken a splendid step towards staying updated with the latest [industry trends, product updates, special offers, etc. – tailor this to match what you'll be sending].\r\n\r\nHere's what you can look forward to:\r\n\r\nExclusive Insights: Be the first to hear about our latest innovations and how they can benefit you.\r\nInspiring Stories: We love to share success stories from our community, and we hope you find them as motivating as we do.\r\nSpecial Offers: Yes, you'll get access to exclusive deals and discounts that are not available anywhere else.\r\n[Any other type of content you plan to send]: [Brief description].\r\nWe promise to only send you emails that we believe will genuinely interest or benefit you. And we’ll always respect your inbox, never overloading it.\r\n\r\nYour journey with us starts here, and we can't wait to share it with you. Keep an eye on your inbox for our welcome gift coming your way soon!\r\n\r\nIf you have any questions, thoughts, or just want to say hi, don't hesitate to reach out to us at this email.\r\n\r\nWelcome aboard,\r\nThe Airline Team<p>",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);
                    client.Send(mailMessage);
                }
            }
            else
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential("maleykaheybatova1011@gmail.com", "luzw ppnb motd bubl");
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage()
                    {
                        From = new MailAddress("maleykaheybatova1011@gmail.com"),
                        Subject = "Welcome to Airline Website",
                        Body = $"Hello {name}, " +
                        $"Thank you for visiting our website. " 
                        ,                        
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);
                    client.Send(mailMessage);
                }
            }
        }
         
    }
}
