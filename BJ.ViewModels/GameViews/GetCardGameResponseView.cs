using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.ViewModels.GameViews
{
    public class GetCardGameResponseView
    {
        public List<BotGetCardGameResponseViewItem> Bots { get; set; }
        public UserGetCardGameResponseView User { get; set; }
        public Guid GameId { get; set; }
    }

    public class BotGetCardGameResponseViewItem
    {
        public string Name { get; set; }
        public int CardsInHand { get; set; }
    }

    public class UserGetCardGameResponseView
    {
        public string Name { get; set; }
        public List<CardGetCardGameResponseViewItem> Cards { get; set; }
        public StateGetCardGameResponseView State { get; set; }
    }

    public class StateGetCardGameResponseView
    {
        public int State { get; set; }
        public string StateAsString { get; set; }
    }

    public class CardGetCardGameResponseViewItem
    {
        public int Suit { get; set; }
        public int Rank { get; set; }
    }
}
