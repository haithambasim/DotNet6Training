namespace Training.Data.Dtos
{
    public class ArticleUpdateDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public long CategoryId { get; set; }
        public List<long> TagIds { get; set; }
        public string ImageName { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
