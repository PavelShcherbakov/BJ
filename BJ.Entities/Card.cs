using BJ.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Card : BaseEntity
    {
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public RankType Rank { get; set; }
        public SuitType Suit { get; set; }
    }
    
}
