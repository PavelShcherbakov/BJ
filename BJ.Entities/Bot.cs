using Dapper.Contrib.Extensions;

namespace BJ.Entities
{
    [Table("Bots")]
    public class Bot : BaseEntity
    {
        public string Name { get; set; }
    }
}
