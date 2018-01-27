using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    [Export(typeof(IEntityTypeConfiguration<CompanyUser>))]
    public class CompanyUserConfiguration : CommonConfiguration, IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable("CompanyUsers");
        }
    }
}
