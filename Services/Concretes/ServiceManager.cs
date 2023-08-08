using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILoginService> _loginService;
        public ILoginService loginService => _loginService.Value;


        public ServiceManager(IRepositoryManager manager)
        {
            _loginService = new Lazy<ILoginService>(() => new LoginService(manager));
        }   
    }
}
