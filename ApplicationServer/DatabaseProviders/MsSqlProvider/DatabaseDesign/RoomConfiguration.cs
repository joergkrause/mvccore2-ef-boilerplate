using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    public class RoomConfiguration : CommonConfiguration<Room>, IGenericConfiguration
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {   
            base.Configure(builder);         
            builder.ToTable("Rooms");            
            builder.Property(p => p.Building).HasMaxLength(12).IsUnicode(false);
            builder.Property(p => p.Number).IsRequired(true);
        }
    }
}
