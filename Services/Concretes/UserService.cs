using Entities.DataModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _manager;


        public UserService(IRepositoryManager manager) =>
            _manager = manager;


        public void CreateUser(User user)
        {
            // create
            _manager
                .UserRepository
                .CreateUser(user);

            _manager.Save();
        }


        public void ControlFormatError(UserView userView)
        {
            var errorCode = "R-FE-";  // (R)egister - (F)ormat (E)rror

            // control
            ControlTelNo(ref errorCode, userView.TelNo);
            ControlEmail(ref errorCode, userView.Email);
            ControlPassword(ref errorCode, userView.Password);

            // when telNo, Email Or Passwords Is Wrong
            if (!errorCode.Equals("R-FE-"))
                throw new Exception(errorCode);
        }


        public void ControlConflictError(UserView userView)
        {
            // get same employees who have same Email or Telno
            var entity = _manager
                .UserRepository
                .GetUsersWithCondition(u =>
                    u.TelNo.Equals(userView.TelNo)
                    || u.Email.Equals(userView.Email)
                , false)
                .ToList();

            // when Email or TelNo already exists
            if (entity.Count != 0)
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


        private void ControlTelNo(ref string errorCode, string telNo)
        {
            // telNo length control
            if (telNo.Length != 11)
                errorCode += "T";  // T : Telephone
        }


        private void ControlEmail(ref string errorCode, string email)
        {
            var emailTags = new List<string>() {
                "@gmail.com",
                "@hotmail.com",
                "@outlook.com"
            };

            // when is wrong
            if (!emailTags.Any(e => email.EndsWith(e)))
                errorCode += "E";  // E: Email
        }


        private void ControlPassword(ref string errorCode, string password)
        {
            var specialChars = new List<char>() { '!', '*', '.', ',', '@', '?'};

            // length Control
            if (password.Length < 6  // min len
                || password.Length > 16)  // max len
                errorCode += "P";  // P: Password

            // char type control
            else if (password.Any(c =>
                !specialChars.Contains(c)  // special chars
                && !(c >= 48 && c <= 57)  // numbers
                && !(c >= 65 && c <= 90)  // big alphabet chars
                && !(c >= 97 && c <= 122)))  // small alphabet chars)
                errorCode += "P";  // P: Password
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


