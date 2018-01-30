using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    public class ProjectConfiguration : CommonConfiguration<Project>, IGenericConfiguration
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);
            builder.ToTable("Projects");
            builder.Property(p => p.Name).HasMaxLength(32).IsRequired(true);
            // Complex Type
            var subType = builder.OwnsOne(p => p.Properties);
            subType.Property(p => p.Start).HasColumnName("ProjectStart");
            subType.Property(p => p.End).HasColumnName("ProjectEnd");

        }
    }
}
