using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace Presentation.Controllers.Api
{
	[ApiController]
	[Route("api/login")]
	public class LoginController : ControllerBase
	{
		private readonly IServiceManager _manager;

		public LoginController(IServiceManager manager) =>
			_manager = manager;

		[HttpPost]
		public async Task<IActionResult> VerifyEmailAndPasswordAsync([FromBody] UserView viewModel)
		{
			try
			{
				await _manager.LoginService.VerifyEmailAndPasswordAsync(viewModel);
				return NoContent();
			}

			catch (Exception ex)
			{
				#region email or password format error
				if (ex.Message.StartsWith("FE"))
					return BadRequest(ex.Message);
				#endregion

				#region email or password verification error
				else if (ex.Message.StartsWith("VE"))
					return NotFound(ex.Message);
				#endregion

				#region unexpected error
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				#endregion
			}
		}

		[HttpPost("sendMail")]
		public async Task<IActionResult> SendMailAsync([FromBody] UserView viewModel)
		{
			try
			{
				await _manager.LoginService.SendMailAsync(viewModel);
				return NoContent();
			}

			catch (Exception ex)
			{
				#region email format error
				if (ex.Message.Equals("FE-E"))
					return BadRequest(new
					{
						errorCode = ex.Message,
						errorDescription = "Format Error - Email"
					});
				#endregion

				#region email verification error
				if (ex.Message.Equals("VE-E"))
					return NotFound(new
					{
						errorCode = "VE-E",
						errorDescription = "Verification Error - Email"
					});
				#endregion

				#region email error
				if (ex.Message.Equals("EE"))
					return StatusCode(204, new
					{
						message = "An error occured when sending email."
					});
				#endregion

				#region unexpected errors
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				#endregion
			}
		}
	}
}
