using Entities.DataModels;
using Repositories.Contracts;
using Repositories.EF;


namespace Repositories.Concretes
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        { }


        public void CreateUser(User user) =>
            base.Create(user);


        public User? FindUserById(int id, bool trackChanges) =>
            base.FindWithCondition(u => u.Id == id, trackChanges)
            .FirstOrDefault();


        public User? FindUserByEmail(string email, bool trackChanges) =>
            base.FindWithCondition(u => u.Email.Equals(email), trackChanges)
            .FirstOrDefault();


        public IQueryable<User> GetAllUsers(bool trackChanges) =>
            base.FindAll(trackChanges);


        public void UpdateUser(User user) =>
            base.Update(user);
        

        public void DeleteUser(User user) =>
            base.Delete(user);
    }
}
