using System;
using System.IO;
using System.Linq;
using JoergIsAGeek.Workshop.DataAccessLayer;
using JoergIsAGeek.Workshop.DatabaseProvider.MsSqlProvider.DatabaseDesign;
using JoergIsAGeek.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JoergIsAGeek.Workshop.Setup
{
    class Program
    {

        static IConfiguration DbConfiguration;

        static void Main(string[] args)
        {
            Console.WriteLine("Setup DB");
            var rc = new RoomConfiguration();
            // appsettings.json based config identical to those used in the service layer
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            DbConfiguration = builder.Build();
            Console.WriteLine($"Create Db for {DbConfiguration.GetConnectionString(nameof(PersonalManagerContext))}");
            // Create DB and check model
            using (var context = new PersonalManagerContext(DbConfiguration)){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                // add seed data for demo
                context.Set<Employee>().Add(new Employee{
                    Name = "Ermin Employee",
                    EmployeeNumber = "A123456"
                });
                context.SaveChanges();
                // force model to be created in memory for testing
                var model = context.Set<Employee>().ToList();
                System.Console.WriteLine(model.Count());
                // for more testing see unit tests
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
