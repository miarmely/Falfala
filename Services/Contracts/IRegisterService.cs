using Entities.DataModels;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRegisterService
    {
        Task CreateUserAsync(User user);
        Task ControlFormatErrorAsync(UserView userView);
        Task ControlConflictErrorAsync(UserView userView);
    }
}
