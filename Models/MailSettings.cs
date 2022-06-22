namespace surgeweb.Models
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class MailAddresses
    {
        public string InfoMail { get; set; }
        public string MainMail { get; set; }
    }
}
