using Entities.DataModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class RegisterService : IRegisterService
    {
        private readonly IRepositoryManager _manager;


        public RegisterService(IRepositoryManager manager) =>
            _manager = manager;


        public async Task CreateUserAsync(User user)
        {
            // create
            _manager.UserRepository
                .CreateUser(user);

            await _manager.SaveAsync();
        }


        public async Task ControlFormatErrorOfUserAsync(UserView userView)
        {
            var errorCode = "R-FE-";  // (R)egister - (F)ormat (E)rror

            // control
            errorCode += IsTelNoSyntaxValid(userView.TelNo)? "" : "T";
            errorCode += await IsEmailSyntaxTrueAsync(userView.Email)? "" : "E";
            errorCode += await IsPasswordSyntaxTrueAsync(userView.Password)? "" : "P";

            // when telNo, Email Or Passwords Is Wrong
            if (!errorCode.Equals("R-FE-"))
                throw new Exception(errorCode);
        }


        public async Task ControlConflictErrorOfUserAsync(UserView userView)
        {
            // get same employees who have same Email or Telno
            var entity = await _manager.UserRepository
                .GetUsersWithConditionAsync(u =>
                    u.TelNo.Equals(userView.TelNo)
                    || u.Email.Equals(userView.Email)
                , false);

            // when Email or TelNo already exists
            if (entity.Count() != 0)
            {
                var errorMessage = "R-CE-";  // (R)egister - (C)onflict (E)rror

                // when TelNo already exists
                if (entity.Any(u => u.TelNo.Equals(userView.TelNo)))
                    errorMessage += "T";

                // when Email already exists
                if (entity.Any(u => u.Email.Equals(userView.Email)))
                    errorMessage += "E";

                throw new Exception(errorMessage);
            }
        }


        public bool IsTelNoSyntaxValid(string telNo)
        {
            // length control
            return telNo.Length == 11;   // T : Telephone
        }


        public async Task<bool> IsEmailSyntaxTrueAsync(string email)
        {
            // when not contain "@"
            return await Task.Run(() =>
                email.Contains("@")
            );
        }


        public async Task<bool> IsPasswordSyntaxTrueAsync(string password)
        {
            var specialChars = new List<char>() { '!', '*', '.', ',', '@', '?', '_' };

            // length Control
            if (password.Length < 6  // min len
                || password.Length > 16)  // max len
                return false;  // P: Password

            // chars control
            var isCharsTypeInvalid = await Task.Run(() =>
            {
                return password.Any(c =>
                    !specialChars.Contains(c)  // special chars
                    && !(c >= 48 && c <= 57)  // numbers
                    && !(c >= 65 && c <= 90)  // big alphabet chars
                    && !(c >= 97 && c <= 122));  // small alphabet chars)
            });

            return isCharsTypeInvalid? false : true;
        }
    }
}


/* ----------------- CONFLICT ERROR CODES ----------------- 
 * R-CE    -> (R)egister - (C)onflict (E)rror
 * 
 * R-CE-T  -> (T)elephone
 * R-CE-E  -> (E)mail
 * R-CE-TE -> (T)elephone + (E)mail
 */


/* ----------------- FORMAT ERROR CODES -----------------
 * R-FE     -> (R)egister - (F)ormat (E)rror
 * R-FE-T   -> (T)elephone
 * R-FE-TE  -> (T)elephone + (E)mail
 * R-FE-TP  -> (T)elephone + (P)assword
 * R-FE-TEP -> (T)elephone + (E)mail + (P)assword
 * R-FE-E   -> (E)mail
 * R-FE-EP  -> (E)mail + (P)assword
 * R-FE-P   -> (P)assword
*/