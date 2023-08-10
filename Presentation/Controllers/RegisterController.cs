using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly IServiceManager _manager;


        public RegisterController(IServiceManager manager) =>
            _manager = manager;


        [HttpPost]
        public IActionResult CreateUser([FromBody] UserView userView)
        {
            try
            {
                // format control
                _manager.RegisterService
                    .ControlFormatError(userView);

                // email, telNo already exists?
                _manager.RegisterService
                    .ControlConflictError(userView);

                // userView convert to user
                var user = _manager.DataConverterService
                    .ConvertToUser(userView);

                // set StatusId
                user.StatusId = _manager.MaritalStatusService
                    .GetMaritalStatusByStatusName(userView.MaritalStatus, false)
                    .Id;

                // create
                _manager.RegisterService
                    .CreateUser(user);

                userView.Id = user.Id;

                return Ok(userView);
            }

            catch (Exception ex)
            {
                // when format or conflict error occured
                if (ex.Message.StartsWith("R-FE-")  // format error
                    || ex.Message.StartsWith("R-CE-"))  // conflict error
                    return BadRequest(ex.Message);

                return NotFound(ex.Message);
            }
        } 
    }
}
