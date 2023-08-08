using Entities.DataModels;
using Repositories.Contracts;
using Repositories.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class LoginRepository : RepositoryBase<Login>, ILoginRepository
    {
        public LoginRepository(RepositoryContext context) : base(context)
        { }


        public Login? FindLogin(string username, string password, bool trackChanges) =>
            base.FindWithCondition(l => 
                l.Username.Equals(username)
                && l.Password.Equals(password)
            , trackChanges)
            .FirstOrDefault();
    }
}
