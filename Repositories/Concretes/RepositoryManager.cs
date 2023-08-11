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
        private readonly RepositoryContext _context;

        private readonly Lazy<IUserRepository> _userRepository;

        private readonly Lazy<IMaritalStatusRepository> _maritalStatusRepository;
        public IUserRepository UserRepository => _userRepository.Value;
        public IMaritalStatusRepository MaritalStatusRepository => _maritalStatusRepository.Value;


        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _maritalStatusRepository = new Lazy<IMaritalStatusRepository>(() => new MaritalStatusRepository(context));
        }


        public async Task SaveAsync() =>
            await _context
            .SaveChangesAsync();
    }
}
