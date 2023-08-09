using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EF.Config
{
    public class MaritalStatusConfig : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            builder.HasData(
                new MaritalStatus() { Id = 1, StatusName = "Bekar" },
                new MaritalStatus() { Id = 2, StatusName = "Evli" },
                new MaritalStatus() { Id = 3, StatusName = "İlişkisi var" },
                new MaritalStatus() { Id = 4, StatusName = "İlişkisi yok" },
                new MaritalStatus() { Id = 5, StatusName = "Karmaşık" });
        }
    }
}
