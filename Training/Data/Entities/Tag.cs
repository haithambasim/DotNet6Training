using Training.Data.Entities.Shared;

namespace Training.Data.Entities
{
    public class Tag : Entity<long>
    {
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
