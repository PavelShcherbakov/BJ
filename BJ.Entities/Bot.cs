using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Bot : BaseEntity
    {
        public string Name { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public int Points { get; set; }
    }
}
