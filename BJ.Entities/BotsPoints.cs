using Dapper.Contrib.Extensions;
using System;

namespace BJ.Entities
{
    [Table("BotsPoints")]
    public class BotsPoints : BaseEntity
    {
        public Guid BotId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("BotId")]
        [Write(false)]
        public virtual Bot Bot { get; set; }

        public Guid GameId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

        public int CardsInHand { get; set; }

        public int Points { get; set; }
    }
}
