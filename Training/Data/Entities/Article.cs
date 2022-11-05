using Training.Data.Entities.Shared;

namespace Training.Data.Entities
{
    public class Article : Entity<long>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public string ImageName { get; set; }
    }
}