using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Data.Entities.Shared
{
    public class Entity<TKey>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey? Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
