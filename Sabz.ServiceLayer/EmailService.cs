using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Sabz.ServiceLayer
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //// Credentials:
            //var credentialUserName = "sabz.Gasht.miad@outlook.com";
            //var sentFrom = "sabz.Gasht.miad@outlook.com";
            //var pwd = "Maryam@1992";
            //// Credentials: 

            //// Configure the client:
            //System.Net.Mail.SmtpClient client =
            //    new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");

            //client.Port = 587;
            //client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //// Creatte the credentials:
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential(credentialUserName, pwd);

            //client.EnableSsl = true;
            //client.Credentials = credentials;

            //// Create the message:
            //var mail =
            //    new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            //mail.Subject = message.Subject;
            //mail.Body = message.Body;

            //// Send:
            //return client.SendMailAsync(mail);
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}