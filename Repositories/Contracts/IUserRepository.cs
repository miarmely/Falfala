using Entities.DataModels;


namespace Repositories.Contracts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        void CreateUser(User user);
        User? FindUserById(int id, bool trackChanges);
        User? FindUserByEmail(string email, bool trackChanges);
        IQueryable<User> GetAllUsers(bool trackChanges);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
