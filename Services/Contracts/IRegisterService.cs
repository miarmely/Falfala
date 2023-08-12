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
        Task ControlFormatErrorOfUserAsync(UserView userView);
        Task ControlConflictErrorOfUserAsync(UserView userView);
        bool IsTelNoSyntaxValid(string telNo);
        Task<bool> IsEmailSyntaxTrueAsync(string email);
        Task<bool> IsPasswordSyntaxTrueAsync(string password);
    }
}
