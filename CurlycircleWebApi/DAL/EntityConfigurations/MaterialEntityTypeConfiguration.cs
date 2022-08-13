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
    public class MaterialEntityTypeConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> materialConfiguration)
        {
            materialConfiguration.ToTable("Materials");

            materialConfiguration.HasKey(x => x.Id);

            materialConfiguration.Property(x => x.Id)
                .UseHiLo("materialseq");
        }
    }
}
