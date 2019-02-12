using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Card : BaseEntity
    {
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
    }

    public enum Rank { Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    public enum Suit { Hearts, Clubs, Diamonds, Spades }
}
