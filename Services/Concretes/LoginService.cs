using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryManager _manager;


        public LoginService(IRepositoryManager manager) =>
            _manager = manager;
       

        public async Task VerifyEmailAndPassword(string email, string password)
        {
            // get employee matched with email
            var entity = await _manager.UserRepository
                .GetUserByEmail(email, false);

            // when email not matched
            if (entity is null)
                throw new Exception("L-VE-E");

            // when password is wrong
            else if (!entity.Password.Equals(password))
                throw new Exception("L-VE-P");
        }
    }
}
/* ------------------ LOGIN ERROR CODE ------------------
 * L-VE    -> (L)ogin - (V)erficiation (E)rror
 * L-VE-E  -> (E)mail
 * L-VE-p  -> (P)assword 
 */
