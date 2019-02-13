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

    public enum Rank { Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 2, Queen = 3, King = 4, Ace = 11 }
    public enum Suit { Hearts, Clubs, Diamonds, Spades }
}
