﻿using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    [Export("GenericConfiguration", typeof(IEntityTypeConfiguration<Project>))]
    public class ProjectConfiguration : CommonConfiguration, IEntityTypeConfiguration<Project>, IGenericConfiguration
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
