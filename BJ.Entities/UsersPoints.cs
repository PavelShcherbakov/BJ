using Dapper.Contrib.Extensions;
using System;

namespace BJ.Entities
{
    [Table("UsersPoints")]
    public class UsersPoints : BaseEntity
    {
        public string UserId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("UserId")]
        [Write(false)]
        public virtual User User { get; set; }

        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

        public int Points { get; set; }
    }
}
