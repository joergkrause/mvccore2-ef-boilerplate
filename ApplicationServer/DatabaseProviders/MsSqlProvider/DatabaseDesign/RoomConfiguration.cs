using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign
{

    [Export(typeof(IEntityTypeConfiguration<Room>))]
    public class RoomConfiguration : CommonConfiguration, IEntityTypeConfiguration<Room>, IRoomConfiguration
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
