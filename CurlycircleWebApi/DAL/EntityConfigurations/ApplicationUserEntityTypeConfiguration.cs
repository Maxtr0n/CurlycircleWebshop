using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfigurations
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> userConfiguration)
        {
            userConfiguration.ToTable("Users");
            userConfiguration.OwnsOne(u => u.Cart).WithOwner(c => c.ApplicationUser);
            userConfiguration.OwnsOne(u => u.Address);
        }
    }
}
