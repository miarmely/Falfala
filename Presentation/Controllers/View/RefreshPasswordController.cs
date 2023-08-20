using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace Presentation.Controllers.View
{
    public class RefreshPasswordController : Controller
    {
        private readonly IServiceManager _manager;

        public RefreshPasswordController(IServiceManager manager) =>
            _manager = manager;

       
        public async Task<IActionResult> Index([FromRoute(Name = "email")] string email)
        {
            try
            {
				#region email format control
				if (!await _manager.UserService
                    .IsEmailSyntaxValidAsync(email))
                    return BadRequest("FE-E");
				#endregion

				#region email control
				var entity = await _manager.UserService
				    .GetUserByEmailAsync(email, false);
				#endregion

				return View(entity);
			}
			catch (Exception ex)
            {
                #region email not matched
                if (ex.Message.Equals("VE-E"))
                    return NotFound("VE-E");
				#endregion

				#region unexpected error
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
				#endregion
			}
		}
    }
}
