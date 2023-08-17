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
        Task CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email, bool trackChanges); 
        Task UpdatePasswordByEmailAsync(UserView viewModel);
		Task ControlFormatErrorOfUserAsync(UserView userView);
        Task ControlConflictErrorOfUserAsync(UserView userView);
        bool IsTelNoSyntaxValid(string telNo);
        Task<bool> IsEmailSyntaxTrueAsync(string email);
        Task<bool> IsPasswordSyntaxTrueAsync(string password);
    }
}
