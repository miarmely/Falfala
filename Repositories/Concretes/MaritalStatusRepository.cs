﻿using Entities.DataModels;
using Repositories.Contracts;
using Repositories.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class MaritalStatusRepository : RepositoryBase<MaritalStatus>, IMaritalStatusRepository
    {

        public MaritalStatusRepository(RepositoryContext context) : base(context)
        { }


        public MaritalStatus GetMaritalStatusByStatusName(string statusName, bool trackChanges) =>
            base.FindWithCondition(m => m.StatusName.Equals(statusName), trackChanges)
            .First();
    }
}
