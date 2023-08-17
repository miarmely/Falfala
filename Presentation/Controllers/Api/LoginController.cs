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
        public async Task<IActionResult> VerifyEmailAndPassword([FromBody] LoginView loginView)
        {
            try
            {
                // verification
                await _manager.LoginService
                    .VerifyEmailAndPassword(loginView.Email, loginView.Password);

                return NoContent();
            }

            catch (Exception ex)
            {
                // when email or password not matched
                if (ex.Message.Equals("VE-E")
                    || ex.Message.Equals("VE-P"))
                    return NotFound(ex.Message);

                // unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("sendMail")]
        public async Task<IActionResult> SendMailAsync([FromBody] IForgetMyPasswordView view)
        {
            try
            {
                // when email format wrong
                if (!await _manager.UserService
                    .IsEmailSyntaxTrueAsync(view.Email))
                    return BadRequest("FE-E");

                // send mail and change password
                await _manager.LoginService
                    .VerifyEmail(view.Email);

                // send mail
                await _manager.MailService
                    .SendMailAsync(new MailView()
                    {
                        To = view.Email,
                        Subject = "Falfala - Şifre Yenileme",
                        Body = @"Yeni bir şifre oluşturmak için lütfen aşağıdaki linke tıklayınız: <br>
                                <a href='http://127.0.0.1:5500/html/newPassword.html'>Link</a>"
                    });

                return NoContent();
            }

            catch (Exception ex)
            {
                // when mail is wrong
                if (ex.Message.Equals("VE-E"))
                    return NotFound("VE-E");

                // for unexpected errors
                return StatusCode(500, ex.Message);
            }
        }
    }
}
