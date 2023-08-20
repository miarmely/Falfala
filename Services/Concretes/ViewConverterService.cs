using Entities.DataModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class ViewConverterService : IViewConverterService
    {
        private readonly IRepositoryManager _manager;

        private readonly IMaritalStatusService _maritalStatusService;

		public ViewConverterService(IRepositoryManager manager, IMaritalStatusService maritalStatusService)
		{
			_manager = manager;
			_maritalStatusService = maritalStatusService;
		}

		public async Task<UserView> ConvertToUserViewAsync(User user)
        {
			#region get maritalStatus
			var maritalStatus = await _maritalStatusService
                .GetMaritalStatusByIdAsync(user.StatusId, false);
			#endregion

			return new UserView()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TelNo = user.TelNo,
                Email = user.Email,
                Age = user.Age,
                Job = user.Job,
                Sex = user.Sex ? "Erkek" : "Kadın",
                BirthDate = user.BirthDate.ToShortDateString(),
                MaritalStatus = maritalStatus.StatusName,
                Password = user.Password
            };
        }
    }
}
