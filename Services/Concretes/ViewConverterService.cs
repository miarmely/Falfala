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


        public ViewConverterService(IRepositoryManager manager) =>
            _manager = manager;
        
            
        public UserView ConvertToUserView(User user)
        {
            return new UserView()
            {
                
            };
        }
    }
}
