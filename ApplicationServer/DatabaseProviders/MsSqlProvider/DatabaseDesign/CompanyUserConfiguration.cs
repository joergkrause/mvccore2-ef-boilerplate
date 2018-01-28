using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    [Export("GenericConfiguration", typeof(IEntityTypeConfiguration<CompanyUser>))]
    public class CompanyUserConfiguration : CommonConfiguration, IEntityTypeConfiguration<CompanyUser>, IGenericConfiguration
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable("CompanyUsers");
        }
    }
}
