using System;
using System.Linq;
using SodgeIt.Workshop.DataAccessLayer;
using SodgeIt.Workshop.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace SodgeIt.Workshop.Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setup DB");
            var connString = @"Server=localhost;Database=SodgeIT;User Id=SA;Password=Ubuntu_Admin;";
            var optionsBuilder = new DbContextOptionsBuilder<PersonalManagerContext>();
            optionsBuilder.UseSqlServer(connString);
            using (var context = new PersonalManagerContext(optionsBuilder.Options)){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Set<Employee>().Add(new Employee{
                    Name = "Ermin Employee",
                    EmployeeNumber = "A123456"
                });
                context.SaveChanges();
                var model = context.Set<Employee>().ToList();
                System.Console.WriteLine(model.Count());
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
