namespace Amara.Microservice.Configuration.Models
{
    public class DBConfig
    {
        public string Host { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? User { get; set; }
        public string? Password { get; set; } = string.Empty;
    }
}
