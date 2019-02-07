using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class UsersStep : BaseEntity
    {
        public int StepNumder { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public User User { get; set; }
    }
}
