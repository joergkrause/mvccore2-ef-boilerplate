using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoergIsAGeek.Workshop.DomainModel.Abstracts
{

    public abstract class AuditableEntityBase : EntityBase
    {

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }


    }
}