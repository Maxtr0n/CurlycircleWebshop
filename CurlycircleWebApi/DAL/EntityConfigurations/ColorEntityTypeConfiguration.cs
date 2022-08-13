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
    public class ColorEntityTypeConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> colorConfiguration)
        {
            colorConfiguration.ToTable("Colors");

            colorConfiguration.HasKey(x => x.Id);

            colorConfiguration.Property(x => x.Id)
                .UseHiLo("colorseq");
        }
    }
}
