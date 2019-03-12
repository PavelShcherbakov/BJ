using BJ.Entities.Enums;
using Dapper.Contrib.Extensions;
using System;


namespace BJ.Entities
{
    [Table("BotsSteps")]
    public class BotsStep : BaseEntity
    {
        public int StepNumder { get; set; }

        public Guid BotId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("BotId")]
        [Write(false)]
        public virtual Bot Bot { get; set; }

        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

        public RankType Rank { get; set; }

        public SuitType Suit { get; set; }
    }
}

