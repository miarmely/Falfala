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
        public async Task<IActionResult> VerifyEmailAndPassword([FromBody] UserView viewModel)
        {
            try
            {
				#region email format control
				if (!await _manager.UserService
					.IsEmailSyntaxTrueAsync(viewModel.Email))
					return BadRequest("FE-E");
				#endregion

				#region password format control
				if (!await _manager.UserService
					.IsPasswordSyntaxTrueAsync(viewModel.Password))
					return BadRequest("FE-P");
				#endregion

				#region verification
				await _manager.LoginService
                    .VerifyEmailAndPassword(viewModel.Email, viewModel.Password);
				#endregion

				return NoContent();
            }

            catch (Exception ex)
            {
				#region email or password format error
				if (ex.Message.StartsWith("FE"))
					return NotFound(ex.Message);
				#endregion

				#region email or password not matched
				if (ex.Message.StartsWith("VE"))
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
                #region email format control
                if (!await _manager.UserService
                    .IsEmailSyntaxTrueAsync(viewModel.Email))
                    return BadRequest("FE-E");
				#endregion

				#region verify email
				await _manager.LoginService
					.VerifyEmail(viewModel.Email);
				#endregion

				#region send mail
				await _manager.MailService
					.SendMailAsync(new MailView()
					{
						To = viewModel.Email,
						Subject = "Falfala - Şifre Yenileme",
						Body = @$"Yeni bir şifre oluşturmak için lütfen aşağıdaki linke tıklayınız: <br>
                                <a href='https://localhost:7131/refreshPassword/index/{viewModel.Email}'>Yeni Şifre Oluştur</a>"
					});
				#endregion

				return NoContent();
            }

            catch (Exception ex)
            {
				#region email format wrong
				if (ex.Message.Equals("VE-E"))
					return NotFound("VE-E");
				#endregion

				#region unexpected errors
				return StatusCode(500, ex.Message);
				#endregion
			}
        }
    }
}
