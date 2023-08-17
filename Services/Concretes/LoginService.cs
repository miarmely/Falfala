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
