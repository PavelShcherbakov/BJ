﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Game : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
