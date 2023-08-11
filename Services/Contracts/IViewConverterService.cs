using Entities.DataModels;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IViewConverterService
    {
        Task<UserView> ConvertToUserViewAsync(User user);
    }
}
