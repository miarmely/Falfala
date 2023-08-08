using Entities.DataModels;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILoginService
    {
        public Login ControlLogin(LoginView loginView, bool trackChanges);
    }
}
