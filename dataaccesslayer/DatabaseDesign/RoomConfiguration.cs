using System;
using Microsoft.EntityFrameworkCore;
using SodgeIt.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SodgeIt.Workshop.DataAccessLayer.DatabaseDesign
{

    //[Export(typeof(IEntityTypeConfiguration))]
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
