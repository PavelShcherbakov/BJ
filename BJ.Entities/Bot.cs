using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    [Table("Bots")]
    public class Bot : BaseEntity
    {
        public string Name { get; set; }
    }
}
