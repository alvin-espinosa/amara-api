namespace Amara.Microservice.Shared.DTOs
{
    public class EmailProvider
    {
        public const string Key = "EmailProvider";
        public string ApiKey { get; set; } = string.Empty;
        public string NoReplyEmail { get; set; } = string.Empty;
    }
}
