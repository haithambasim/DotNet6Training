namespace Training.Data.Dtos
{
    public class ArticleCreateDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public long CategoryId { get; set; }
    }
}
