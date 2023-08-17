namespace Entities.ConfigModels
{
    public class MailSettingsConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? From { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; }
        public bool UseSSL { get; set; }
        public bool UseTLS { get; set; }
    }
}
