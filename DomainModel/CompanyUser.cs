using System;
using System.Collections.Generic;
using System.Linq;

namespace JoergIsAGeek.Workshop.DomainModel
{
    public abstract class CompanyUser : AuditableEntityBase
    {

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public bool HasProjects(){
            return Projects.Any();
        }

    }
}