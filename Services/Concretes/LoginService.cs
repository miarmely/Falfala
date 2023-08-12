using Entities.DataModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;
using System.Net;
using System.Net.Mail;

namespace Services.Concretes
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryManager _manager;


        public LoginService(IRepositoryManager manager) =>
            _manager = manager;


        public async Task VerifyEmailAndPassword(string email, string password)
        {
            var entity = await VerifyEmail(email);

            // when password is wrong
            if (!entity.Password.Equals(password))
                throw new Exception("VE-P");
        }


        public async Task<User> VerifyEmail(string email)
        {
            // get user by matched with email
            var entity = await _manager.UserRepository
                .GetUserByEmailAsync(email, false);

            // when email not matched
            if (entity is null)
                throw new Exception("VE-E");

            return entity;
        }


        private async Task SendMailAsync(string from, string to, string subject, string body)
        {
            //set mail message
            var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            //mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            // set mail settings
            var smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("kamondosteam6@gmail.com", "gamarcoba08");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            // send mail
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
/* ------------------ LOGIN ERROR CODE ------------------
 * L-VE     -> (L)ogin - (V)erficiation (E)rror
 * L-VE-E   -> (E)mail is wrong
 * L-VE-p   -> (P)assword is wrong
 * 
 * L-IFMP-FE   -> (L)ogin - (I) (F)orgot (M)y (P)assword - (F)ormat (E)rror
 * L-IFMP-FE-E -> (E)mail format is invalid
 * L-IFMP-VE   -> (L)ogin - (I) (F)orgot (M)y (P)assword - (V)erification (E)rror
 * L-IFMP-VE-E -> (E)mail not found
 */
