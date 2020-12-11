﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Web.Services
{
    public class EmailSender: IEmailSender
    {
        public IConfiguration Configuration { get; }
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, IConfiguration configuration)
        {
            Configuration = configuration;
            Options = optionsAccessor.Value;
        }
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("contato@upfirst.com.br", "UpFirst"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            IConfigurationSection sendGridAuthNSection =
                        Configuration.GetSection("Authentication:SendGrid");
            //throw new NotImplementedException();
            return Execute(sendGridAuthNSection["apiKey"], subject, htmlMessage, email);
        }
    }
}