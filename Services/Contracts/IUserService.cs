using Entities.DataModels;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        void CreateUser(User user);
        public void ControlFormatError(UserView userView);
        public void ControlConflictError(UserView userView);
    }
}
