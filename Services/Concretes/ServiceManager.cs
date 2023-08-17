using Entities.ConfigModels;
using Microsoft.Extensions.Options;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class ServiceManager : IServiceManager
    {
        
        private readonly Lazy<IUserService> _userService;
        
        private readonly Lazy<IDataConverterService> _dataConverterService;

        private readonly Lazy<IViewConverterService> _viewConverterService;

        private readonly Lazy<IMaritalStatusService> _maritalStatusService;
        
        private readonly Lazy<ILoginService> _loginService;

        private readonly Lazy<IMailService> _mailService;
        public IUserService UserService => _userService.Value;
        public IDataConverterService DataConverterService => _dataConverterService.Value;
        public IViewConverterService ViewConverterService => _viewConverterService.Value;
        public IMaritalStatusService MaritalStatusService => _maritalStatusService.Value;
        public ILoginService LoginService => _loginService.Value;
        public IMailService MailService => _mailService.Value;


        public ServiceManager(IRepositoryManager manager, IOptions<MailSettingsConfig> mailSettings)
        {
            _userService = new Lazy<IUserService>(() => new UserService(manager));
            _dataConverterService = new Lazy<IDataConverterService>(() => new DataConverterService(manager));
            _viewConverterService = new Lazy<IViewConverterService>(() => new ViewConverterService(manager));
            _maritalStatusService = new Lazy<IMaritalStatusService>(() => new MaritalStatusService(manager));
            _loginService = new Lazy<ILoginService>(() => new LoginService(manager));
            _mailService = new Lazy<IMailService>(() => new MailService(mailSettings));
        }
    }
}
