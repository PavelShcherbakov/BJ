using BJ.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class UsersStep : BaseEntity
    {
        public int StepNumder { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public Guid GameId { get; set; }
        public RankType Rank { get; set; }
        public SuitType Suit { get; set; }
    }
}

