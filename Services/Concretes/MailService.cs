using Entities.ConfigModels;
using Entities.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Contracts;


namespace Services.Concretes
{
    public class MailService : IMailService
    {
        private readonly MailSettingsConfig _mailConfig;


        public MailService(IOptions<MailSettingsConfig> config) =>
            _mailConfig = config.Value;


        public async Task SendMailAsync(MailView mailView)
        {
            var mimeMessage = new MimeMessage();

            #region set "Sender"
            // set "From"
            mimeMessage.From.Add(new MailboxAddress(
                    _mailConfig.DisplayName, _mailConfig.From));

            // set "Sender"
            mimeMessage.Sender = new MailboxAddress(_mailConfig.DisplayName, _mailConfig.From);
            #endregion

            #region set "To"
            // set "To" if in List
            if (mailView.ToAsList is not null)
                foreach (var mail in mailView.ToAsList)
                    mimeMessage.To.Add(
                        MailboxAddress.Parse(mail));
            
            // set "To"
            else
                mimeMessage.To.Add(
                    MailboxAddress.Parse(mailView.To));
            #endregion

            #region set "Subject" and "Body"
            // set "Subject"
            mimeMessage.Subject = mailView.Subject;

            // set "Body"
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailView.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            #endregion

            #region send mail
            // send mail
            using (var smtpClient = new SmtpClient())
            {
                // connect with TLS  ->  (NEW Version) Encryption Type
                if (_mailConfig.UseTLS)
                    await smtpClient.ConnectAsync(_mailConfig.Host, _mailConfig.Port, SecureSocketOptions.StartTls);

                // connect with SSL  ->  (OLD Version) Encryption Type
                else if (_mailConfig.UseSSL)
                    await smtpClient.ConnectAsync(_mailConfig.Host, _mailConfig.Port, SecureSocketOptions.SslOnConnect);

                // authentication
                await smtpClient.AuthenticateAsync(
                    _mailConfig.Username, _mailConfig.Password);

                // send mail
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
            #endregion
        }
    }
}
