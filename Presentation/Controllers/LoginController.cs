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
        public IActionResult VerifyEmailAndPassword([FromBody] LoginView loginView)
        {
            try
            {
                // verification
                _manager.LoginService
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
                return StatusCode(500, "Server Error");
            }
        }
    }
}
