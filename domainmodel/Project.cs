namespace SodgeIt.Workshop.DomainModel
{
    public class Project : EntityBase
    {
        public string Name { get; set; }

        public ProjectProperty Properties { get; set; }

    }
}