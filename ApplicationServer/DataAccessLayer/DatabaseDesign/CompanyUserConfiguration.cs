using System;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoergIsAGeek.Workshop.DataAccessLayer.DatabaseDesign
{

    //[Export(typeof(IEntityTypeConfiguration))]
    public class CompanyUserConfiguration : CommonConfiguration, IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable("CompanyUsers");
        }
    }
}
