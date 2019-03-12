using BJ.Entities.Enums;
using Dapper.Contrib.Extensions;
using System;


namespace BJ.Entities
{
    [Table("UsersSteps")]
    public class UsersStep : BaseEntity
    {
        public int StepNumder { get; set; }

        public string UserId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("UserId")]
        [Write(false)]
        public virtual User User { get; set; }

        public Guid GameId { get; set; }

        public RankType Rank { get; set; }

        public SuitType Suit { get; set; }
    }
}

