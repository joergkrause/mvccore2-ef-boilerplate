using JoergIsAGeek.Workshop.DomainModel.Abstracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoergIsAGeek.Workshop.DataAccessLayer.DatabaseDesign {
    public class CommonConfiguration {
        public void ConfigureBase<T> (EntityTypeBuilder<T> builder) where T : EntityBase {
            builder.HasKey (p => p.Id);
            builder.Property (p => p.RowVersion).IsRowVersion();
            builder.Property (p => p.Id).ValueGeneratedOnAdd ();
        }

    }
}