using System;
using System.Collections.Generic;

namespace BJ.ViewModels.GameViews
{
    public class EndGameResponseView
    {
        public List<BotEndGameResponseViewItem> Bots { get; set; }
        public UserEndGameResponseView User { get; set; }
        public Guid GameId { get; set; }
    }

    public class BotEndGameResponseViewItem
    {
        public string Name { get; set; }
        public int CardsInHand { get; set; }
    }

    public class UserEndGameResponseView
    {
        public string Name { get; set; }
        public List<CardEndGameResponseViewItem> Cards { get; set; }
        public StateEndGameResponseView State { get; set; }
    }

    public class StateEndGameResponseView
    {
        public int State { get; set; }
        public string StateAsString { get; set; }
    }

    public class CardEndGameResponseViewItem
    {
        public int Suit { get; set; }
        public int Rank { get; set; }
    }
}
