using System;
using Microsoft.EntityFrameworkCore;

namespace JoergIsAGeek.CoreWorkshop.DataAccessLayer
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {

        private string connectionString;


        public DbSet<UserModel> Users { get; set; }

        public DatabaseContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(this.connectionString);            
        }

        

    }
}
