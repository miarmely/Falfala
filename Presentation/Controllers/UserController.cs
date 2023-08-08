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
    [Route("api/register")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;


        public UserController(IServiceManager manager) =>
            _manager = manager;



         
           
            
        

    }
}
