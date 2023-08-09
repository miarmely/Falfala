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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;


        public UserController(IServiceManager manager) =>
            _manager = manager;


        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] UserView userView)
        {
            try
            {
                // format control
                _manager
                    .UserService
                    .ControlFormatError(userView);

                // email, telNo already exists?
                _manager
                    .UserService
                    .ControlConflictError(userView);

                // userView convert to user
                var user = _manager
                    .DataConverterService
                    .ConvertToUser(userView);

                // set StatusId
                user.StatusId = _manager
                    .MaritalStatusService
                    .GetMaritalStatusByStatusName(userView.MaritalStatus, false)
                    .Id;

                // create
                _manager
                    .UserService
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
