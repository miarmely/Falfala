using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EF;
using System.Linq.Expressions;


namespace Repositories.Concretes
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        { }


        public void CreateUser(User user) =>
            base.Create(user);


        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
            await base.FindAll(trackChanges)
            .OrderBy(u => u.Id)
            .ToListAsync();


        public async Task<IEnumerable<User>> GetUsersWithConditionAsync(Expression<Func<User, bool>> findExpression, bool trackChanges) =>
            await base.FindWithCondition(findExpression, trackChanges)
            .OrderBy(u => u.Id)
            .ToListAsync();


        public async Task<User?> GetUserByIdAsync(int id, bool trackChanges) =>
            await base.FindWithCondition(u => u.Id == id, trackChanges)
            .FirstOrDefaultAsync();


        public async Task<User?> GetUserByEmailAsync(string email, bool trackChanges) =>
            await base.FindWithCondition(u => u.Email.Equals(email), trackChanges)
            .FirstOrDefaultAsync();


        public void UpdateUser(User user) =>
            base.Update(user);


        public void DeleteUser(User user) =>
            base.Delete(user);
    }
}
