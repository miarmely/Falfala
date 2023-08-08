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
    public class LoginConfig : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.HasData(
                new Login() { Id = 1, Username = "mostmert", Password = "mert123" },
                new Login() { Id = 2, Username = "mostfurkan", Password = "furkan123" },
                new Login() { Id = 3, Username = "mosteda", Password = "eda123" },
                new Login() { Id = 4, Username = "mosterce", Password = "erce123" }
                );
        }
    }
}
