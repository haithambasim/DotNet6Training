namespace Training.Data.Dtos
{
    public class ArticleDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }
        public List<TagDto> Tags { get; set; }
        public string ImageName { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
