using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Services
{
    public class EmailProcessor
    {
        public static void SendInvitation(string emailToSend, string userName, string url)
        {
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                var from = new MailAddress("escobar.tipson@gmail.com", "WhereToGo");
                var to = new MailAddress(emailToSend);

                var message = new MailMessage(from, to)
                {
                    Subject = "Invitation",
                    Body = $"Hello\n" +
                    $"User '{userName}' invite you to event, follow the link: \n" +
                    $"<a href='{url}' title='Look the event!'>go to the event page</a>",
                    IsBodyHtml = true
                };

                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("escobar.tipson@gmail.com", "rfat_gnfx");

                smtp.Send(message);
            }
        }
    }
}
