using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace RoyalVilla_API.Helper
{
    public class EmailSender : IEmailSender
    {
        private readonly MailjetSetting _mailjetSetting;
        
        public EmailSender(IOptions<MailjetSetting> mailjetsettings)
        {
            _mailjetSetting = mailjetsettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(_mailjetSetting.PublicKey,_mailjetSetting.PrivateKey)
            {
                //Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", _mailjetSetting.Email},
        {"Name", "Devang"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Devang"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "TextPart",
       "My first Mailjet email"
      },{"HTMLPart",htmlMessage}, {
       "CustomID",
       "AppGettingStartedTest"
      }
     }
             });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
