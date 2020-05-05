using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Twilio; 

namespace Sabz.ServiceLayer
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //string AccountSid = "AC579c0374771204f788d50cfa1faccf88";
            //string AuthToken = "cbc7b82917800293e4648f6a4618bbc9";
            //string twilioPhoneNumber = "12028481401";

            //var twilio = new TwilioRestClient(AccountSid, AuthToken);
            //twilio.SendMessage(twilioPhoneNumber, message.Destination, message.Body);


            // Twilio does not return an async Task, so we need this:
            return Task.FromResult(0);
        }
    }
}
