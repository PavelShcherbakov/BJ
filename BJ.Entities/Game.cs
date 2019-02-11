using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class Game : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        private User user;
        public virtual User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                UserId = user.Id;
            }
        }
    }
}
