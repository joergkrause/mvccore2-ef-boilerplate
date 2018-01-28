using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JoergIsAGeek.Workshop.DataAccessLayer.ControlModels;
using JoergIsAGeek.Workshop.DomainModel;
using JoergIsAGeek.Workshop.DomainModel.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JoergIsAGeek.Workshop.DataAccessLayer {
    public class PersonalManagerContext : DbContext {

        // externally provided connection string
        private readonly string connString;
        // path to an assembly with EF configurations
        private readonly string configurationAssemblyPath;
        // The method used to add database connection
        private readonly string configurationUseDatabase;

        public PersonalManagerContext (IConfiguration config) : base () {
            connString = config.GetConnectionString (nameof (PersonalManagerContext));
            // in appsettings.json just provide the path and name of the config assembly
            configurationAssemblyPath = config["ConfigurationAssemblyPath"];
            configurationUseDatabase = config["ConfigurationUseDatabase"];
        }

        public PersonalManagerContext (DbContextOptions<PersonalManagerContext> options) : base (options) { }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            var defaultProvider = "";
            // In case the service layer calls this we just provide the connection string and config assembly
            if (!String.IsNullOrEmpty (connString) && !String.IsNullOrEmpty(configurationUseDatabase)) {
                switch (configurationUseDatabase) {
                    case "UseSqlServer":                
                        optionsBuilder.UseSqlServer (connString);
                        defaultProvider = "MsSqlProvider.dll";
                        break;
                    case "UseMySqlServer":                
                        optionsBuilder.UseMySql (connString);
                        defaultProvider = "MySqlProvider.dll";
                        break;
                    default:
                    throw new ArgumentOutOfRangeException($"Unknown database provider {configurationUseDatabase}.");
                }
            }
            // scan assemblies
            var folder = Path.GetDirectoryName(this.GetType().Assembly.Location);
            Assembly confAssembly = Assembly.LoadFile (Path.Combine(folder, configurationAssemblyPath ?? defaultProvider));
            var config = new ContainerConfiguration ().WithAssembly (confAssembly);
            using (var container = config.CreateContainer ()) {
                // fill in the exports where we have the [ImportMany] attribute on
                EntityConfigurations = new List<IEntityTypeConfiguration<EntityBase>>();
                EntityConfigurations.Add((IEntityTypeConfiguration<EntityBase>)container.GetExport<IEntityTypeConfiguration<Room>>("GenericConfiguration"));
                EntityConfigurations.Add((IEntityTypeConfiguration<EntityBase>)container.GetExport<IEntityTypeConfiguration<Project>>("GenericConfiguration"));
                EntityConfigurations.Add((IEntityTypeConfiguration<EntityBase>)container.GetExport<IEntityTypeConfiguration<CompanyUser>>("GenericConfiguration"));
            }            
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Employee> ().ToTable ("CompanyUsers");
            modelBuilder.Entity<ExternalUser> ().ToTable ("CompanyUsers");
            LoadConfigurations (modelBuilder);
            base.OnModelCreating (modelBuilder);
        }

        private void LoadConfigurations (ModelBuilder modelBuilder) {
            ((IEnumerable<IEntityTypeConfiguration<EntityBase>>)EntityConfigurations)
                .ToList()
                .ForEach (e => modelBuilder.ApplyConfiguration (e));
        }

        /// <summary>
        /// External assemblies have exports of the base type 
        /// </summary>
        /// <returns></returns>
        private ICollection<IEntityTypeConfiguration<EntityBase>> EntityConfigurations { get; set; }


        public ErrorModel Save (string userName = null) {
            SaveTasks (userName);
            ErrorModel result = new ErrorModel ();
            try {
                Validation ();
                var saveResult = base.SaveChanges ();
                result.Result = saveResult;
            } catch (DbUpdateConcurrencyException cex) {
                result.Message = cex.Message;
                result.InnerException = cex;
            } catch (DbUpdateException uex) {
                result.Message = uex.Message;
                result.InnerException = uex;
            } catch (ValidationException vex) {
                result.Message = vex.Message;
                result.InnerException = vex;
            } finally { }
            return result;
        }

        public async Task<ErrorModel> SaveAsync (string userName = null) {
            SaveTasks (userName);
            ErrorModel result = new ErrorModel ();
            try {
                Validation ();
                var saveResult = await base.SaveChangesAsync ();
                result.Result = saveResult;
            } catch (DbUpdateConcurrencyException cex) {
                result.Message = cex.Message;
                result.InnerException = cex;
            } catch (DbUpdateException uex) {
                result.Message = uex.Message;
                result.InnerException = uex;
            } catch (ValidationException vex) {
                result.Message = vex.Message;
                result.InnerException = vex;
            } finally { }
            return result;
        }

        /// <summary>
        /// Validate all entities for both, attributes and fluent syntax.
        /// </summary>
        /// <exception>ValidationException</exception>
        private void Validation () {
            foreach (var entity in ChangeTracker.Entries ().Where (e => e.State == EntityState.Added || e.State == EntityState.Modified)) {
                var validationContext = new ValidationContext (entity);
                Validator.ValidateObject (entity, validationContext, true);
            }
        }

        /// <summary>
        /// Set common values for objects in change tracker
        /// </summary>
        /// <param name="userName"></param>
        private void SaveTasks (string userName) {
            var now = DateTime.Now;
            // Track base history data for auditable models
            foreach (var item in ChangeTracker.Entries<AuditableEntityBase> ()) {
                switch (item.State) {
                    case EntityState.Added:
                        item.Entity.CreatedAt = now;
                        item.Entity.CreatedBy = userName ?? "System";
                        goto
                    case EntityState.Modified;
                    case EntityState.Modified:
                        item.Entity.ModifiedAt = now;
                        item.Entity.ModifiedBy = userName ?? "System";
                        break;
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                        // TODO: Add what you need here
                        break;
                }
            }
        }

    }
}