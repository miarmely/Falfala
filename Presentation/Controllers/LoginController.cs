using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
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
                await _manager
                    .LoginService
                    .VerifyEmailAndPassword(loginView.Email, loginView.Password);

                return NoContent();
            }

            catch (Exception ex)
            {
                // when email or password not matched
                if (ex.Message.Equals("L-VE-E")
                    || ex.Message.Equals("L-VE-P"))
                    return NotFound(ex.Message);

                // unexpected errors
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("sendMail")]
        public async Task<IActionResult> SendMailAsync([FromBody] IForgetMyPasswordView view)
        {
            try
            {
                // when email format wrong
                if (!await _manager.RegisterService
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
                        Subject = "Email Verification",
                        Body = @"<b>This</b> is my <a href='https://google.com'>Link</a>"
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
