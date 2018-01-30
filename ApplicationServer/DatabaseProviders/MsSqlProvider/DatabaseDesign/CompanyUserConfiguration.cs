using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    public class CompanyUserConfiguration : CommonConfiguration<CompanyUser>, IGenericConfiguration
    {
        public override void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            base.Configure(builder);
            builder.ToTable("CompanyUsers");
        }
    }
}
