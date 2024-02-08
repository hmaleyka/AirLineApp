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
                    $"<p>Welcome to AirLine App, your ultimate destination for style inspiration and exclusive updates! As a valued subscriber, you're in for a treat. Get ready to elevate your inbox with a curated selection of fashion insights, lifestyle trends, and insider news tailored specifically for you. Delight in early access to our latest collections, limited-time offers, and exclusive promotions, all delivered directly to your inbox. Your preferences matter, and we're committed to providing a personalized experience that matches your unique taste. Join our vibrant community, share your thoughts, and engage with fellow subscribers. Your privacy is important to us, and we ensure the security of your information. Expect surprises – from exciting giveaways to special treats, we love to spoil our subscribers. Get set for a subscription journey filled with elegance, sophistication, and endless style possibilities. Embrace your individuality with Diana, where every newsletter is a celebration of your distinct persona.<p>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                client.Send(mailMessage);
            }
        }
    }
}
