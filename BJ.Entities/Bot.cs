using Dapper.Contrib.Extensions;
using System;

namespace BJ.Entities
{
    [Table("Bots")]
    public class Bot : BaseEntity
    {
        public string Name { get; set; }
        //public Guid GameId { get; set; }
        //[ForeignKey("GameId")]
        //public virtual Game Game { get; set; }
        //public int Points { get; set; }
    }
}
