﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public static class SendConfirmationService
    {
        public static void SendEmail(string to, string url)
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
                    Body = $"Hello please confirm your email with this url <a href='{url}'</a>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                client.Send(mailMessage);
            }

        }
    }
}
