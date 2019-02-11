using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    public class UsersStep : BaseEntity
    {
        public int StepNumder { get; set; }
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
        public Guid GameId { get; set; }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
    }
}

