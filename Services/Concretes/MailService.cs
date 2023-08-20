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

        private readonly ILoggerService _logger;

        public MailService(IOptions<MailSettingsConfig> config, ILoggerService logger)
        {
			_mailConfig = config.Value;
            _logger = logger;
		}

        public async Task SendMailAsync(MailView mailView)
        {
            var mimeMessage = new MimeMessage();

            #region set "Sender"
            // set "From"
            mimeMessage.From
                .Add(new MailboxAddress(_mailConfig.DisplayName, _mailConfig.From));

            // set "Sender"
            mimeMessage.Sender = new MailboxAddress(_mailConfig.DisplayName, _mailConfig.From);
            #endregion

            #region set "To"
            // more than one
            if (mailView.ToAsList is not null)
                foreach (var mail in mailView.ToAsList)
                    mimeMessage.To.Add(
                        MailboxAddress.Parse(mail));
            
            // one person
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
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    #region connect with TLS  ->  (NEW Version) Encryption Type
					if (_mailConfig.UseTLS)
                        await smtpClient.ConnectAsync(_mailConfig.Host, _mailConfig.Port, SecureSocketOptions.StartTls);
					#endregion

					#region connect with SSL  ->  (OLD Version) Encryption Type
					else if (_mailConfig.UseSSL)
						await smtpClient.ConnectAsync(_mailConfig.Host, _mailConfig.Port, SecureSocketOptions.SslOnConnect);
					#endregion

					#region authentication
					await smtpClient.AuthenticateAsync(
						_mailConfig.Username, _mailConfig.Password);
					#endregion

					#region send mail
					await smtpClient.SendAsync(mimeMessage);
					await smtpClient.DisconnectAsync(true);
                    #endregion
                }
                catch (Exception ex)
                {
                    #region when sending email to one person
                    if (mailView.ToAsList is null)
                    {
                        _logger.LogError($"Error Occured When Sending Email. -> email:{mailView.To}");
                        throw new Exception("EE");
					}
					#endregion

					#region when sending email to more than one person
					else
					{
						var emailListAsString = "";

						//  transport emails in list to string
						mailView.ToAsList.ForEach(email =>
							emailListAsString += $"{email} ");

						_logger.LogError($"Error Occured When Sending Email -> emails:{emailListAsString}");
                        throw new Exception("EE");
                    }
					#endregion
				}
			}
            #endregion
        }
    }
}
/* ---------------- Error Codes ---------------- 
 * EE -> Email Error
*/
