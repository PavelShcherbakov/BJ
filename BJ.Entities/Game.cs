using BJ.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Game : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int CountStep { get; set; }
        public int NumberOfPlayers { get; set; }
        public UserGameStateType State { get; set; }
    }
    
}
