using Entities.DataModels;
using Entities.ErrorModels;
using Entities.Exceptions;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;
using System.Runtime.InteropServices;


namespace Services.Concretes
{
	public class LoginService : ILoginService
	{
		private readonly IRepositoryManager _manager;

		private readonly ILoggerService _logger;

		private readonly IUserService _userService;

		private readonly IMailService _mailService;

		public LoginService(IRepositoryManager manager
			, ILoggerService logger
			, IUserService userService
			, IMailService mailService)
		{
			_manager = manager;
			_logger = logger;
			_userService = userService;
			_mailService = mailService;
		}

		public async Task VerifyEmailAndPasswordAsync(UserView viewModel)
		{
			#region format control and verify email
			await EmailOrPasswordFormatControlAsync(viewModel.Email, viewModel.Password);
			var entity = await VerifyEmailAsync(viewModel.Email);
			#endregion

			#region password verification error 
			if (!entity.Password.Equals(viewModel.Password))
				throw new VerificationErrorException("VE-P"
					, "Verification Error - Password"
					, $"Password not matched. email:{entity.Email} && passsword:{viewModel.Password}");
			#endregion
		}
	
		public async Task EmailOrPasswordFormatControlAsync([Optional] string email
			, [Optional] string password)
		{
			#region email format control
			if (email != null
				&& !await _userService.IsEmailSyntaxValidAsync(email))
			{
				_logger.LogInfo($"Format Error - Email -> email:{email}");
				throw new Exception("FE-E");
			}
			#endregion

			#region password format control
			if (password != null
				&& !await _userService.IsPasswordSyntaxValidAsync(password))
			{
				_logger.LogInfo($"Format Error - Password -> email:{email} password:{password}");
				throw new Exception("FE-P");
			}
			#endregion
		}

		public async Task<User> VerifyEmailAsync(string email)
		{
			#region get user by email
			var user = await _manager.UserRepository
				.GetUserByEmailAsync(email, false);
			#endregion

			#region  email not matched
			if (user == null)
			{
				_logger.LogInfo($"Verification Error - Email -> email:{email}");
				throw new Exception("VE-E");
			}
			#endregion

			return user;
		}

		public async Task SendMailAsync(UserView viewModel)
		{
			#region format control and verify email
			await EmailOrPasswordFormatControlAsync(viewModel.Email);
			await VerifyEmailAsync(viewModel.Email);
			#endregion

			#region send mail
			await _mailService.SendMailAsync(new MailView()
			{
				To = viewModel.Email,
				Subject = "Falfala - Şifre Yenileme",
				Body = @$"Yeni bir şifre oluşturmak için lütfen aşağıdaki linke tıklayınız: <br>
									<a href='https://localhost:7131/refreshPassword/index/{viewModel.Email}'>Yeni Şifre Oluştur</a>"
			});
			#endregion
		}
	}
}
/* ------------------ LOGIN ERROR CODE ------------------
 * L-VE     -> (L)ogin - (V)erficiation (E)rror
 * L-VE-E   -> (E)mail is wrong
 * L-VE-p   -> (P)assword is wrong
 * 
 * L-IFMP-FE   -> (L)ogin - (I) (F)orgot (M)y (P)assword - (F)ormat (E)rror
 * L-IFMP-FE-E -> (E)mail format is invalid
 * L-IFMP-VE   -> (L)ogin - (I) (F)orgot (M)y (P)assword - (V)erification (E)rror
 * L-IFMP-VE-E -> (E)mail not found
 */
