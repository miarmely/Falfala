using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IMaritalStatusService MaritalStatusService { get; }
        IDataConverterService DataConverterService { get; }
        IViewConverterService ViewConverterService { get; }
        ILoginService LoginService { get;  }
        IMailService MailService { get; }
    }
}
