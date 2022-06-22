namespace surgeweb.Models
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
    public class QuoteRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
