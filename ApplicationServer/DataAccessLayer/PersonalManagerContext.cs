﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JoergIsAGeek.Workshop.DataAccessLayer.ControlModels;
using JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign;
using JoergIsAGeek.Workshop.DatabaseProviders.ProviderInterfaces;
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
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Employee> ().ToTable ("CompanyUsers");
            modelBuilder.Entity<ExternalUser> ().ToTable ("CompanyUsers");
            LoadConfigurations (modelBuilder);
            base.OnModelCreating (modelBuilder);
        }

        private void LoadConfigurations (ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyUserConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }

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