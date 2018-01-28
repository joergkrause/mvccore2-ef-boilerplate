using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    [Export("GenericConfiguration", typeof(IEntityTypeConfiguration<Room>))]
    public class RoomConfiguration : CommonConfiguration, IEntityTypeConfiguration<Room>, IGenericConfiguration
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {            
            builder.ToTable("Rooms");
            base.ConfigureBase(builder);
            builder.Property(p => p.Building).HasMaxLength(12).IsUnicode(false);
            builder.Property(p => p.Number).IsRequired(true);
        }
    }
}
