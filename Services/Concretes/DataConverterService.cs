using Entities.DataModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class DataConverterService : IDataConverterService
    {
        private readonly IRepositoryManager _manager;


        public DataConverterService(IRepositoryManager manager) =>
            _manager = manager;
        

        public User ConvertToUser(UserView userView)
        {
            // set sex
            var sex = userView.Sex.Equals("Erkek") ? true : false;
                
            return new User()
            {
                Id = Convert.ToInt32(userView.Id),
                FullName = userView.FullName,
                LastName = userView.LastName,
                TelNo = userView.TelNo,
                Email = userView.Email,
                Age = Convert.ToInt32(userView.Age),
                Job = userView.Job,
                Sex = sex,
                BirthDate = Convert.ToDateTime(userView.BirthDate),
                StatusId = 0,
                Password = userView.Password
            };
        }
    }
}
