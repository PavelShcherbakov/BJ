using BJ.Entities.Enums;
using Dapper.Contrib.Extensions;

namespace BJ.Entities
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        public string UserId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("UserId")]
        [Write(false)]
        public virtual User User { get; set; }

        public int CountStep { get; set; }

        public int NumberOfPlayers { get; set; }

        public UserGameStateType State { get; set; }
    }

}
