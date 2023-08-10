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
        
        private readonly Lazy<IRegisterService> _registerService;
        
        private readonly Lazy<IDataConverterService> _dataConverterService;

        private readonly Lazy<IViewConverterService> _viewConverterService;

        private readonly Lazy<IMaritalStatusService> _maritalStatusService;
        
        private readonly Lazy<ILoginService> _loginService;
        public IRegisterService RegisterService => _registerService.Value;
        public IDataConverterService DataConverterService => _dataConverterService.Value;
        public IViewConverterService ViewConverterService => _viewConverterService.Value;
        public IMaritalStatusService MaritalStatusService => _maritalStatusService.Value;
        public ILoginService LoginService => _loginService.Value;


        public ServiceManager(IRepositoryManager manager)
        {
            _registerService = new Lazy<IRegisterService>(() => new RegisterService(manager));
            _dataConverterService = new Lazy<IDataConverterService>(() => new DataConverterService(manager));
            _viewConverterService = new Lazy<IViewConverterService>(() => new ViewConverterService(manager));
            _maritalStatusService = new Lazy<IMaritalStatusService>(() => new MaritalStatusService(manager));
            _loginService = new Lazy<ILoginService>(() => new LoginService(manager));
        }
    }
}
