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
        public UserGameState State { get; set; }
    }
    public enum UserGameState { InGame, Lose, Win };
}
