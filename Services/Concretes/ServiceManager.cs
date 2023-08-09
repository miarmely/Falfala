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
        
        private readonly Lazy<IUserService> _userService;
        
        private readonly Lazy<IDataConverterService> _dataConverterService;

        private readonly Lazy<IViewConverterService> _viewConverterService;

        private readonly Lazy<IMaritalStatusService> _maritalStatusService;
        public IUserService UserService => _userService.Value;
        public IDataConverterService DataConverterService => _dataConverterService.Value;
        public IViewConverterService ViewConverterService => _viewConverterService.Value;
        public IMaritalStatusService MaritalStatusService => _maritalStatusService.Value;


        public ServiceManager(IRepositoryManager manager)
        {
            _userService = new Lazy<IUserService>(() => new UserService(manager));
            _dataConverterService = new Lazy<IDataConverterService>(() => new DataConverterService(manager));
            _viewConverterService = new Lazy<IViewConverterService>(() => new ViewConverterService(manager));
            _maritalStatusService = new Lazy<IMaritalStatusService>(() => new MaritalStatusService(manager));
        }
    }
}
