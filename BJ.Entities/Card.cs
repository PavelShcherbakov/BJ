using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Card:BaseEntity
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }

    public enum Rank { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    public enum Suit { Hearts, Clubs, Diamonds, Spades }
}
