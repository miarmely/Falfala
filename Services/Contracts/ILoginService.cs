using Entities.DataModels;
using Entities.ViewModels;
using System.Runtime.InteropServices;


namespace Services.Contracts
{
    public interface ILoginService
    {
        Task VerifyEmailAndPasswordAsync(UserView viewModel);
		Task EmailOrPasswordFormatControlAsync([Optional] string email, [Optional] string password);
		Task<User> VerifyEmailAsync(string email);
		Task SendMailAsync(UserView viewModel);
	}
}
