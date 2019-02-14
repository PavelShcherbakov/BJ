using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BJ.Entities
{
    public class BotsPoints:BaseEntity
    {
        public Guid BotId { get; set; }
        [ForeignKey("BotId")]
        public virtual Bot Bot { get; set; }

        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public int Points { get; set; }
    }
}
