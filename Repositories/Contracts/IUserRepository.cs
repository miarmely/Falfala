using Entities.DataModels;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        Task CreateUser(User user);
        IQueryable<User> GetAllUsers(bool trackChanges);
        IQueryable<User> GetUsersWithCondition(Expression<Func<User, bool>> findExpression, bool trackChanges);
        User? GetUserById(int id, bool trackChanges);
        Task<User?> GetUserByEmail(string email, bool trackChanges);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
