using System.ComponentModel.DataAnnotations;
using Training.Validation;

namespace Training.Data.Dtos
{
    public class ArticleCreateDto
    {
        [MinLength(length: 10, ErrorMessage = "Please give a valid title, title must be at least 10 charecters")]
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        [XssCheck]
        public string Content { get; set; }
        public long CategoryId { get; set; }
        public List<long> TagIds { get; set; }
        public string ImageName { get; set; }
    }
}
