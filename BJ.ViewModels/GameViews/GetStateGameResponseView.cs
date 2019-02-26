using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.ViewModels.GameViews
{
    public class GetStateGameResponseView
    {
        public List<BotGetStateGameResponseViewItem> Bots { get; set; }
        public UserGetStateGameResponseView User { get; set; }
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
        public int State { get; set; }
    }

    public class CardGetStateGameResponseViewItem
    {
        public int Suit { get; set; }
        public int Rank { get; set; }
    }
    
}
