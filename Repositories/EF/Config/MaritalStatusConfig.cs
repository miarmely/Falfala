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
                new MaritalStatus() { Id = 1, Status = "Bekar" },
                new MaritalStatus() { Id = 2, Status = "Evli" },
                new MaritalStatus() { Id = 3, Status = "İlişkisi var" },
                new MaritalStatus() { Id = 4, Status = "İlişkisi yok" },
                new MaritalStatus() { Id = 5, Status = "Karmaşık" });
        }
    }
}
