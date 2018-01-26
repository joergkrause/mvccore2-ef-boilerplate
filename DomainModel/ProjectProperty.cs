using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoergIsAGeek.Workshop.DomainModel
{

    [ComplexType]
    public class ProjectProperty
    {

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool Critical { get; set; }

        [NotMapped]
        public TimeSpan Duration {
            get{
                return End.Subtract(Start);
            }
        }

    }
}