using System.Composition;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign {

    /// <summary>
    /// Base configuration for all models.
    /// </summary>
    public abstract class CommonConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey (p => p.Id);
            builder.Property (p => p.RowVersion).IsRowVersion();
            builder.Property (p => p.Id).ValueGeneratedOnAdd ();
        }

    }
}