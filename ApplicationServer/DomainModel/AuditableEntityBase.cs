using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoergIsAGeek.Workshop.DomainModel
{

    public abstract class AuditableEntityBase : EntityBase
    {

        //[Column("CA", Order=1, TypeName="")]
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }


    }
}