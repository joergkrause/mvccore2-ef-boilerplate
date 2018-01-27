using JoergIsAGeek.Workshop.DomainModel.Abstracts;

namespace JoergIsAGeek.Workshop.DomainModel
{
    public class Project : EntityBase
    {
        public string Name { get; set; }

        public ProjectProperty Properties { get; set; }

    }
}