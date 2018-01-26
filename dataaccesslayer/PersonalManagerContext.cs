using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SodgeIt.Workshop.DataAccessLayer.DatabaseDesign;
using SodgeIt.Workshop.DomainModel;

namespace SodgeIt.Workshop.DataAccessLayer
{
    public class PersonalManagerContext : DbContext
    {

        private readonly string connString;

        public PersonalManagerContext(IConfiguration config) 
            : base()
        {            
            connString = config.GetConnectionString(nameof(PersonalManagerContext));
        }

        public PersonalManagerContext(DbContextOptions<PersonalManagerContext> options) 
            : base(options)
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
//            if (sql)
                optionsBuilder.UseSqlServer(connString);
//            if(sqlit)
//                optionsBuilder.UseSqlite(connString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // TPH
            modelBuilder.Entity<CompanyUser>().ToTable("CompanyUsers");
            modelBuilder.Entity<Employee>().ToTable("CompanyUsers");
            modelBuilder.Entity<ExternalUser>().ToTable("CompanyUsers");
            // TPT
            modelBuilder.Entity<Project>().ToTable("Projects");
            // Complex Type
            var subType = modelBuilder.Entity<Project>().OwnsOne(p => p.Properties);
            subType.Property(p => p.Start).HasColumnName("ProjectStart");
            subType.Property(p => p.End).HasColumnName("ProjectEnd");

            // Id
            modelBuilder.Entity<Project>().HasKey(p => p.Id);
            modelBuilder.Entity<Project>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CompanyUser>().HasKey(p => p.Id);
            modelBuilder.Entity<CompanyUser>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var name = "Administrator";
            var now = DateTime.Now;
            foreach (var item in ChangeTracker.Entries<AuditableEntityBase>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedAt = now;
                        item.Entity.CreatedBy = name;
                        goto case EntityState.Modified;
                    case EntityState.Modified:
                        item.Entity.ModifiedAt = now;
                        item.Entity.ModifiedBy = name;
                        break;
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            var result = 0;
            try
            {
                result = base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException cex) when (cex.Entries.Count > 2)
            {
                // TODO:
            }
            catch (DbUpdateException uex)
            {
                // TODO:
            }
            finally
            {
            }
            return result;
        }


    }
}
