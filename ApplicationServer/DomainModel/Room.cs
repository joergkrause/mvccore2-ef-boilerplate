using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DomainModel
{
    public class Room : EntityBase
    {
        public string Number { get; set; }

        public string Building { get; set; }
    }
}