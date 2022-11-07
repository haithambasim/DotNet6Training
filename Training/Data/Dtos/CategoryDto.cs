namespace Training.Data.Dtos
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
