using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class BotsStep : BaseEntity
    {
        public int StepNumder { get; set; }
        public Guid BotId { get; set; }
        [ForeignKey("BotId")]
        private Bot bot;
        public virtual Bot Bot
        {
            get
            {
                return bot;
            }
            set
            {
                bot = value;
                BotId = bot.Id;
                GameId = bot.GameId;
            }
        }
        public Guid GameId { get; set; }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
    }
}

