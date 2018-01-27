using System;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoergIsAGeek.Workshop.DataAccessLayer.DatabaseDesign
{

    //[Export(typeof(IEntityTypeConfiguration))]
    public class ProjectConfiguration : CommonConfiguration, IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            base.ConfigureBase(builder);
            builder.Property(p => p.Name).HasMaxLength(32).IsRequired(true);
            // Complex Type
            var subType = builder.OwnsOne(p => p.Properties);
            subType.Property(p => p.Start).HasColumnName("ProjectStart");
            subType.Property(p => p.End).HasColumnName("ProjectEnd");

        }
    }
}
