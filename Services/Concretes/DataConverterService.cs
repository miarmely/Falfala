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


		public async Task<User> ConvertToUserAsync(UserView modelView)
		{
			#region set status
			var status = modelView.MaritalStatus == null ?
				default
				: await _manager.MaritalStatusRepository
				 .GetMaritalStatusByNameAsync(modelView.MaritalStatus, false);
			#endregion

			return new User()
			{
				Id = modelView.Id ?? default,
				FirstName = modelView.FirstName,
				LastName = modelView.LastName,
				TelNo = modelView.TelNo,
				Email = modelView.Email,
				Age = modelView.Age ?? default,
				Job = modelView.Job,
				#region Sex
				Sex = modelView.Sex.Equals("Erkek") ?
						true
						: false,
				#endregion
				BirthDate = Convert.ToDateTime(modelView.BirthDate),
				StatusId = status.Id,
				Password = modelView.Password
			};
		}
	}
}
