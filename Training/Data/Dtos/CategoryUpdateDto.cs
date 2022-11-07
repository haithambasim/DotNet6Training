namespace Training.Data.Dtos
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
