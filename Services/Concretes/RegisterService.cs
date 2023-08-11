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


        public async Task ControlFormatErrorAsync(UserView userView)
        {
            var errorCode = "R-FE-";  // (R)egister - (F)ormat (E)rror

            // control
            errorCode += ControlTelNo(userView.TelNo);
            errorCode += await ControlEmailAsync(userView.Email);
            errorCode += await ControlPasswordAsync(userView.Password);

            // when telNo, Email Or Passwords Is Wrong
            if (!errorCode.Equals("R-FE-"))
                throw new Exception(errorCode);
        }


        public async Task ControlConflictErrorAsync(UserView userView)
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


        private string ControlTelNo(string telNo)
        {
            // length control
            return telNo.Length == 11 ? "" : "T";   // T : Telephone
        }


        private async Task<string> ControlEmailAsync(string email)
        {
            // when not contain "@"
            return await Task.Run(() =>
                email.Contains("@") ? "" : "E"
            );
        }


        private async Task<string> ControlPasswordAsync(string password)
        {
            var specialChars = new List<char>() { '!', '*', '.', ',', '@', '?', '_' };

            // length Control
            if (password.Length < 6  // min len
                || password.Length > 16)  // max len
                return "P";  // P: Password

            // chars control
            var isCharsTypeInvalid = await Task.Run(() =>
            {
                return password.Any(c =>
                    !specialChars.Contains(c)  // special chars
                    && !(c >= 48 && c <= 57)  // numbers
                    && !(c >= 65 && c <= 90)  // big alphabet chars
                    && !(c >= 97 && c <= 122));  // small alphabet chars)
            });


            return isCharsTypeInvalid ? "P" : "";
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
 * 
 * R-FE-T   -> (T)elephone
 * R-FE-TE  -> (T)elephone + (E)mail
 * R-FE-TP  -> (T)elephone + (P)assword
 * R-FE-TEP -> (T)elephone + (E)mail + (P)assword
 * R-FE-E   -> (E)mail
 * R-FE-EP  -> (E)mail + (P)assword
 * R-FE-P   -> (P)assword

*/