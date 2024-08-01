namespace Amara.Microservice.Shared.Interface
{
    public interface IEmailService
    {
        Task<bool> SendAsync(
            string subject,
            string content,
            string toEmails,
            CancellationToken cancellationToken,
            string bccEmails = null,
            string ccEmails = null
            );

        Task<bool> SendAsync(
            string subject,
            string content,
            string[] toEmails,
            CancellationToken cancellationToken,
            string[] bccEmails = null,
            string[] ccEmails = null);
    }
}
