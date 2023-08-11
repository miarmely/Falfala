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
        public async Task<IActionResult> CreateUser([FromBody] UserView userView)
        {
            try
            {
                // format control
                await _manager.RegisterService
                    .ControlFormatErrorAsync(userView);

                // email, telNo already exists?
                await _manager.RegisterService
                    .ControlConflictErrorAsync(userView);

                // userView convert to user
                var user = await _manager.DataConverterService
                    .ConvertToUserAsync(userView);

                // set marital status id
                var maritalStatus = await _manager.MaritalStatusService
                    .GetMaritalStatusByStatusNameAsync(userView.MaritalStatus, false);

                user.StatusId = maritalStatus.Id;

                // create
                await _manager.RegisterService
                    .CreateUserAsync(user);

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
