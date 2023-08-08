using Repositories.Contracts;
using Repositories.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<ILoginRepository> _loginRepository;
        public ILoginRepository loginRepository => _loginRepository.Value;
        

        public RepositoryManager(RepositoryContext context)
        {
            _loginRepository = new Lazy<ILoginRepository>(() => new LoginRepository(context));           
        }
    }
}
