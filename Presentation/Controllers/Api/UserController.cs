using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Net;

namespace Presentation.Controllers.Api
{
	[ApiController]
	[Route("api/user")]
	public class UserController : ControllerBase
	{
		private readonly IServiceManager _manager;

		public UserController(IServiceManager manager) =>
			_manager = manager;

		[HttpPost]
		public async Task<IActionResult> CreateUserAsync([FromBody] UserView userView)
		{
			try
			{
				await _manager.UserService
					.CreateUserAsync(userView);

				return StatusCode(StatusCodes.Status201Created, userView);
			}

			catch (Exception ex)
			{
				#region email format error
				if (ex.Message.StartsWith("FE"))  // format error
					return BadRequest(ex.Message);
				#endregion

				#region conflict error
				else if (ex.Message.StartsWith("CE"))
					return Conflict(ex.Message);  // 409
				#endregion

				#region unexpected errors
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				#endregion
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePasswordAsync([FromBody] UserView viewModel)
		{
			try
			{
				var userView = await _manager.UserService
					.UpdatePasswordAsync(viewModel);

				return Ok(userView);
			}
			catch (Exception ex)
			{
				#region email verification error
				if (ex.Message.StartsWith("VE"))
					return NotFound(ex.Message);
				#endregion

				#region unexpected errors
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				#endregion
			}
		}
	}
}
