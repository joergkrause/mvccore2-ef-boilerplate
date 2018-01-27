using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoergIsAGeek.Workshop.DomainModel.Abstracts
{
    public abstract class EntityBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public byte[] RowVersion { get; set; }


    }
}
