using Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ILoginRepository : IRepositoryBase<Login>
    {
        Login? FindLogin(string username, string password, bool trackChanges);
    }
}
