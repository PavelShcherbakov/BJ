using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Deck : BaseEntity
    {
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        private Game game;
        public virtual Game Game
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
                GameId = game.Id;
            }
        }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
    }

    public enum Rank { Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    public enum Suit { Hearts, Clubs, Diamonds, Spades }
}
