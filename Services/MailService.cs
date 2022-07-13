using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using surgeweb.IService;
using surgeweb.Models;

namespace surgeweb.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly MailAddresses _mailAddresses;
        public MailService(IOptions<MailSettings> mailSettings, IOptions<MailAddresses> mailAddresses)
        {
            _mailSettings = mailSettings.Value;
            _mailAddresses=mailAddresses.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailWithTemplate(MailRequest mailRequest)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\RequestCallBack.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[Name]", mailRequest.Name).Replace("[Email]", mailRequest.ToEmail).Replace("[PhoneNumber]", mailRequest.PhoneNumber).Replace("[body]",mailRequest.Body);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(_mailAddresses.InfoMail));
                email.Bcc.Add(MailboxAddress.Parse(_mailAddresses.MainMail));
                email.Subject = $"Call back request";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task SendEmailQuoteWithTemplate(QuoteRequest mailRequest)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\RequestQuote.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[Name]", mailRequest.Name).Replace("[Email]", mailRequest.ToEmail).Replace("[PhoneNumber]", mailRequest.PhoneNumber).Replace("[body]", mailRequest.Body).Replace("[productType]", mailRequest.ProductType);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(_mailAddresses.InfoMail));
                email.Bcc.Add(MailboxAddress.Parse(_mailAddresses.MainMail));
                email.Subject = $"Quote request";//$"Welcome {mailRequest.Subject}";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
