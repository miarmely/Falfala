using Entities.DataModels;
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


        public IQueryable<User> GetAllUsers(bool trackChanges) =>
            base.FindAll(trackChanges)
            .OrderBy(u => u.Id);


        public IQueryable<User> GetUsersWithCondition(Expression<Func<User, bool>> findExpression, bool trackChanges) =>
            base.FindWithCondition(findExpression, trackChanges)
            .OrderBy(u => u.Id);


        public User? GetUserById(int id, bool trackChanges) =>
            base.FindWithCondition(u => u.Id == id, trackChanges)
            .FirstOrDefault();


        public User? GetUserByEmail(string email, bool trackChanges) =>
            base.FindWithCondition(u => u.Email.Equals(email), trackChanges)
            .FirstOrDefault();


        public void UpdateUser(User user) =>
            base.Update(user);


        public void DeleteUser(User user) =>
            base.Delete(user);
    }
}
