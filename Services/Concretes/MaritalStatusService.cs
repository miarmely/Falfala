using Entities.DataModels;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class MaritalStatusService : IMaritalStatusService
    {
        private readonly IRepositoryManager _manager;

        public MaritalStatusService(IRepositoryManager manager) =>
            _manager = manager;

		public async Task<MaritalStatus> GetMaritalStatusByNameAsync(string statusName, bool trackChanges) =>
            await _manager.MaritalStatusRepository
                .GetMaritalStatusByNameAsync(statusName, trackChanges);
        

        public async Task<MaritalStatus> GetMaritalStatusByIdAsync(int statusId, bool trackChanges) => 
            await _manager.MaritalStatusRepository
                .GetMaritalStatusByIdAsync(statusId, trackChanges);
	}
}
