using Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IMaritalStatusRepository : IRepositoryBase<MaritalStatus>
    {
        Task<MaritalStatus> GetMaritalStatusByStatusNameAsync(string statusName, bool trackChanges);
    }
}
