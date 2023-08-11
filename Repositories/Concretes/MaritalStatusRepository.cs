using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class MaritalStatusRepository : RepositoryBase<MaritalStatus>, IMaritalStatusRepository
    {

        public MaritalStatusRepository(RepositoryContext context) : base(context)
        { }


        public async Task<MaritalStatus> GetMaritalStatusByStatusNameAsync(string statusName, bool trackChanges) =>
            await base.FindWithCondition(m => m.StatusName.Equals(statusName), trackChanges)
            .FirstAsync();
    }
}
