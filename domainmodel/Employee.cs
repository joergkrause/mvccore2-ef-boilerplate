using System;
using System.ComponentModel.DataAnnotations;

namespace SodgeIt.Workshop.DomainModel
{
    public class Employee : CompanyUser
    {

        public string EmployeeNumber { get; set; }

        // Fall 1: Raum zwingend
        // [Required]
        // public Room Room { get; set; } = new Room();

        // Fall 2: Raum optional
        public Room Room { get; set; }

    }
}