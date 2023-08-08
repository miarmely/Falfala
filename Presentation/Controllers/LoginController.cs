using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


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
        public IActionResult ControlLogin([FromBody] LoginView loginView)
        {
            try
            {
                // check informations
                _manager
                    .loginService
                    .ControlLogin(loginView, false);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Not Found": return NotFound("User Not Found");
                    case "Null Argument": return BadRequest("Invalid Argument");
                }
                    
                throw new Exception(ex.Message);
            }
        }
    }
}
