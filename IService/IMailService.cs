using surgeweb.Models;

namespace surgeweb.IService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmailWithTemplate(MailRequest mailRequest);
        Task SendEmailQuoteWithTemplate(QuoteRequest quoteRequest);
    }
}
