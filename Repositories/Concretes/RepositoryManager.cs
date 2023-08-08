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

        public RepositoryManager(RepositoryContext context)
        {
        }
    }
}
