using Training.Data.Entities.Shared;

namespace Training.Data.Entities
{
    public class Category : Entity<long>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
