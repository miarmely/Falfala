using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace Presentation.Controllers.View
{
    public class RefreshPasswordController : Controller
    {
        private readonly IServiceManager _manager;


        public RefreshPasswordController(IServiceManager manager) =>
            _manager = manager;


        public IActionResult Index()
        {
            return View();
        }
    }
}
