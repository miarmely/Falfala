using Entities.DataModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryManager _manager;


        public LoginService(IRepositoryManager manager) =>
            _manager = manager;
        

        public Login ControlLogin(LoginView loginView, bool trackChanges)
        {
            // when username or password is null
            if (loginView.Username is null
                || loginView.Password is null)
                throw new Exception("Null Argument");

            // find Login
            var entity = _manager
                .loginRepository
                .FindLogin(loginView.Username, loginView.Password, trackChanges);

            // when not matched
            if (entity is null)
                throw new Exception("Not Found");

            return entity;
        }
    }
}
