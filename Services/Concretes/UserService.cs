using Entities.ConfigModels;
using Entities.DataModels;
using Entities.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Repositories.Contracts;
using Services.Contracts;
using System.Runtime.CompilerServices;

namespace Services.Concretes
{
	public class UserService : IUserService
	{
		private readonly IRepositoryManager _manager;

		private readonly ILoggerService _logger;

		private readonly IDataConverterService _dataConverterService;

		private readonly IViewConverterService _viewConverterService;

		private readonly IMaritalStatusService _maritalStatusService;

		private readonly UserSettingsConfig _userSettings;

		public UserService(IRepositoryManager manager
			, ILoggerService logger
			, IDataConverterService dataConverterService
			, IViewConverterService viewConverterService
			, IMaritalStatusService maritalStatusService
			, IOptions<UserSettingsConfig> userSettings)
		{
			_manager = manager;
			_logger = logger;
			_dataConverterService = dataConverterService;
			_viewConverterService = viewConverterService;
			_maritalStatusService = maritalStatusService;
			_userSettings = userSettings.Value;
		}

		public async Task CreateUserAsync(UserView viewModel)
		{
			#region format and conflict error control
			await ControlFormatErrorOfUserAsync(viewModel);
			await ControlConflictErrorOfUserAsync(viewModel);
			#endregion

			#region convert viewModel to dataModel
			var user = await _dataConverterService
				.ConvertToUserAsync(viewModel);
			#endregion

			#region add statusId
			// set marital status id
			var maritalStatus = await _maritalStatusService
				.GetMaritalStatusByNameAsync(viewModel.MaritalStatus, false);

			user.StatusId = maritalStatus.Id;
			#endregion

			#region create user
			_manager.UserRepository
				.CreateUser(user);

			await _manager.SaveAsync();

			// add id
			viewModel.Id = user.Id;
			#endregion
		}

		public async Task<User?> GetUserByEmailAsync(string email, bool trackChanges)
		{
			#region get employee
			var entity = await _manager.UserRepository
				.GetUserByEmailAsync(email, trackChanges);
			#endregion

			#region when employee not matched
			if (entity == null)
			{
				_logger.LogInfo($"Verification Error - Email -> email:{email}");
				throw new Exception("VE-E");
			}
			#endregion

			return entity;
		}

		public async Task<UserView> UpdatePasswordAsync(UserView viewModel)
		{
			var user = await GetUserByEmailAsync(viewModel.Email, false);

			#region update password of user
			user.Password = viewModel.Password;

			_manager.UserRepository
				.UpdateUser(user);

			await _manager.SaveAsync();
			#endregion

			#region get new userView
			var userView = await _viewConverterService
				.ConvertToUserViewAsync(user);
			#endregion

			return userView;
		}

		public async Task ControlFormatErrorOfUserAsync(UserView userView)
		{
			#region set error code
			var shortErrorCode = "FE-";
			var longErrorCode = "Format Error - ";

			// control tel no
			if (!IsTelNoSyntaxValid(userView.TelNo))
			{
				shortErrorCode += "T";
				longErrorCode += "Telephone ";
			}

			// control email
			if (!await IsEmailSyntaxValidAsync(userView.Email))
			{
				shortErrorCode += "E";
				longErrorCode += "Email ";
			}

			// control password
			if (!await IsPasswordSyntaxValidAsync(userView.Password))
			{
				shortErrorCode += "P";
				longErrorCode += "Password ";
			}
			#endregion

			#region when telNo, email or passwords is invalid
			if (!shortErrorCode.Equals("FE-"))
			{
				_logger.LogInfo(longErrorCode);
				throw new Exception(shortErrorCode);
			}
			#endregion
		}

		public async Task ControlConflictErrorOfUserAsync(UserView userView)
		{
			#region get employees that matched same email or telNo
			var entity = await _manager.UserRepository
				.GetUsersWithConditionAsync(u =>
					u.TelNo.Equals(userView.TelNo)
					|| u.Email.Equals(userView.Email)
				, false);
			#endregion

			#region when email or telNo already exists
			if (entity.Count() != 0)
			{
				var shortErrorCode = "CE-";
				var longErrorCode = "Conflict Error - ";

				// control TelNo
				if (entity.Any(u => u.TelNo.Equals(userView.TelNo)))
				{
					shortErrorCode += "T";
					longErrorCode += "Telephone ";
				}
					
				// control Email
				if (entity.Any(u => u.Email.Equals(userView.Email)))
				{
					shortErrorCode += "E";
					longErrorCode += "Email ";
				}

				// throw error
				_logger.LogInfo(longErrorCode);
				throw new Exception(shortErrorCode);
			}
			#endregion
		}

		public bool IsTelNoSyntaxValid(string telNo)
		{
			// length control
			return telNo.Length == _userSettings.TelNoLength;
		}

		public async Task<bool> IsEmailSyntaxValidAsync(string email)
		{
			return await Task.Run(() =>
			{
				#region when '@' not matched
				var index = email.IndexOf('@');

				if (index == -1)
					return false;
				#endregion

				#region when '.' not matched
				var emailExtension = email.Substring(index + 1);  // ex: gmail.com

				if (!emailExtension.Contains('.'))
					return false;
				#endregion

				return true;
			});
		}

		public async Task<bool> IsPasswordSyntaxValidAsync(string password)
		{
			#region length Control
			if (password.Length < _userSettings.MinPasswordLength  // min len
				|| password.Length > _userSettings.MaxPasswordLength)  // max len
				return false;
			#endregion

			#region chars control
			var isCharsTypeInvalid = await Task.Run(() =>
			{
				return password.Any(c =>
					!_userSettings.SpecialChars.Contains(c.ToString())  // special chars
					&& !(c >= 48 && c <= 57)  // numbers
					&& !(c >= 65 && c <= 90)  // big alphabet chars
					&& !(c >= 97 && c <= 122));  // small alphabet chars)
			});
			#endregion

			return isCharsTypeInvalid ? false : true;
		}
	}
}
/* ----------------- CONFLICT ERROR CODES ----------------- 
 * CE    -> (C)onflict (E)rror
 * CE-T  -> (T)elephone
 * CE-E  -> (E)mail
 * CE-TE -> (T)elephone + (E)mail
 */


/* ----------------- FORMAT ERROR CODES -----------------
 * FE     -> (F)ormat (E)rror
 * FE-T   -> (T)elephone
 * FE-TE  -> (T)elephone + (E)mail
 * FE-TP  -> (T)elephone + (P)assword
 * FE-TEP -> (T)elephone + (E)mail + (P)assword
 * FE-E   -> (E)mail
 * FE-EP  -> (E)mail + (P)assword
 * FE-P   -> (P)assword
*/