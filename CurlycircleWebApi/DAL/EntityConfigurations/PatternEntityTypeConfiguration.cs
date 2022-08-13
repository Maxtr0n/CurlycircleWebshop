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
    public class PatternEntityTypeConfiguration : IEntityTypeConfiguration<Pattern>
    {
        public void Configure(EntityTypeBuilder<Pattern> patternConfiguration)
        {
            patternConfiguration.ToTable("Patterns");

            patternConfiguration.HasKey(x => x.Id);

            patternConfiguration.Property(x => x.Id)
                .UseHiLo("patternseq");
        }
    }
}
