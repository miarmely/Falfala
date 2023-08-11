using Entities.DataModels;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        void CreateUser(User user);
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        Task<IEnumerable<User>> GetUsersWithConditionAsync(Expression<Func<User, bool>> findExpression, bool trackChanges);
        Task<User?> GetUserByIdAsync(int id, bool trackChanges);
        Task<User?> GetUserByEmailAsync(string email, bool trackChanges);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
