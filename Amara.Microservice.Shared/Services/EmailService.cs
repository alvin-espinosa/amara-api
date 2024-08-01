using Amara.Microservice.Shared.DTOs;
using Amara.Microservice.Shared.Interface;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Amara.Microservice.Shared
{
    public class EmailService : IEmailService
    {
        private readonly EmailProvider emailProvider;

        public EmailService(IOptions<EmailProvider> emailProviderOptions)
        {
            if (emailProviderOptions is null) throw new ArgumentNullException(nameof(emailProviderOptions));

            this.emailProvider = emailProviderOptions.Value;
        }

        public async Task<bool> SendAsync(
          string subject,
          string content,
          string toEmail,
          CancellationToken cancellationToken,
          string? bccEmail = null,
          string? ccEmail = null
          )
        {
            var sendGridMessage = new SendGridMessage();

            if (ccEmail is not null)
            {
                sendGridMessage.AddBcc(ccEmail);
            }

            if (bccEmail is not null)
            {
                sendGridMessage.AddBcc(ccEmail);
            }

            sendGridMessage.SetFrom(emailProvider.NoReplyEmail);
            sendGridMessage.AddTo(toEmail);
            sendGridMessage.SetSubject(subject);
            sendGridMessage.AddContent(MimeType.Html, content);

            return await SendEmailAsync(sendGridMessage, cancellationToken);
        }

        public async Task<bool> SendAsync(
            string subject,
            string content,
            string[] toEmails,
            CancellationToken cancellationToken,
            string[] bccEmails = null,
            string[] ccEmails = null)
        {
            var sendGridMessage = new SendGridMessage();

            sendGridMessage.SetFrom(emailProvider.NoReplyEmail);

            foreach (var email in toEmails)
            {
                sendGridMessage.AddTo(email);
            }

            foreach (var email in bccEmails)
            {
                sendGridMessage.AddCc(email);
            }

            foreach (var email in ccEmails)
            {
                sendGridMessage.AddBcc(email);
            }

            sendGridMessage.SetSubject(subject);

            sendGridMessage.AddContent(MimeType.Html, content);

            return await SendEmailAsync(sendGridMessage, cancellationToken);
        }

        private async Task<bool> SendEmailAsync(SendGridMessage sendGridMessage, CancellationToken cancellationToken)
        {
            try
            {
                var sendGridClient = new SendGridClient(emailProvider.ApiKey);

                var response = await sendGridClient.SendEmailAsync(sendGridMessage, cancellationToken);

                return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
            }
            catch
            {
                throw;
            }
        }
    }
}
