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


        public MaritalStatus GetMaritalStatusByStatusName(string statusName, bool trackChanges)
        {
            return _manager
                .MaritalStatusRepository
                .GetMaritalStatusByStatusName(statusName, trackChanges);
        }
    }
}
