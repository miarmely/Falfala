using Entities.DataModels;
using Entities.ViewModels;


namespace Services.Contracts
{
    public interface IUserService
    {
        Task CreateUserAsync(UserView viewModel);
        Task<User?> GetUserByEmailAsync(string email, bool trackChanges); 
        Task<UserView> UpdatePasswordAsync(UserView viewModel);
		Task ControlFormatErrorOfUserAsync(UserView userView);
        Task ControlConflictErrorOfUserAsync(UserView userView);
        bool IsTelNoSyntaxValid(string telNo);
        Task<bool> IsEmailSyntaxValidAsync(string email);
        Task<bool> IsPasswordSyntaxValidAsync(string password);
    }
}
