using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace Presentation.Controllers.Api
{
	[ApiController]
	[Route("api/register")]
	public class UserController : ControllerBase
	{
		private readonly IServiceManager _manager;


		public UserController(IServiceManager manager) =>
			_manager = manager;


		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserView userView)
		{
			try
			{
				// format control
				await _manager.UserService
					.ControlFormatErrorOfUserAsync(userView);

				// email, telNo already exists?
				await _manager.UserService
					.ControlConflictErrorOfUserAsync(userView);

				// userView convert to user
				var user = await _manager.DataConverterService
					.ConvertToUserAsync(userView);

				// set marital status id
				var maritalStatus = await _manager.MaritalStatusService
					.GetMaritalStatusByStatusNameAsync(userView.MaritalStatus, false);

				user.StatusId = maritalStatus.Id;

				// create
				await _manager.UserService
					.CreateUserAsync(user);

				userView.Id = user.Id;

				return StatusCode(StatusCodes.Status201Created, userView);
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


		[HttpGet]
		public IActionResult Trying()
		{
			return Ok();
		}
	}
}
