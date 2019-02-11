using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Bot : BaseEntity
    {
        public string Name { get; set; }
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
    }
}
