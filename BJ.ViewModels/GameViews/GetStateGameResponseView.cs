using System;
using System.Collections.Generic;

namespace BJ.ViewModels.GameViews
{
    public class GetStateGameResponseView
    {
        public List<BotGetStateGameResponseViewItem> Bots { get; set; }
        public UserGetStateGameResponseView User { get; set; }
        public Guid GameId { get; set; }
    }

    public class BotGetStateGameResponseViewItem
    {
        public string Name { get; set; }
        public int CardsInHand { get; set; }
    }

    public class UserGetStateGameResponseView
    {
        public string Name { get; set; }
        public List<CardGetStateGameResponseViewItem> Cards { get; set; }
        public StateGetStateGameResponseView State { get; set; }
    }

    public class StateGetStateGameResponseView
    {
        public int State { get; set; }
        public string StateAsString { get; set; }
    }

    public class CardGetStateGameResponseViewItem
    {
        public int Suit { get; set; }
        public int Rank { get; set; }
    }

}
