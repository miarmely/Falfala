using Entities.DataModels;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        void CreateUser(User user);
        IQueryable<User> GetAllUsers(bool trackChanges);
        IQueryable<User> GetUsersWithCondition(Expression<Func<User, bool>> findExpression, bool trackChanges);
        User ? GetUserById(int id, bool trackChanges);
        User? GetUserByEmail(string email, bool trackChanges);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
