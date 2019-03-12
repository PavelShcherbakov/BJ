using BJ.Entities.Enums;
using Dapper.Contrib.Extensions;
using System;


namespace BJ.Entities
{
    [Table("Decks")]
    public class Card : BaseEntity
    {
        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

        public RankType Rank { get; set; }

        public SuitType Suit { get; set; }
    }
    
}
