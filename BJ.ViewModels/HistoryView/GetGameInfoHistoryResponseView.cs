using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.ViewModels.HistoryView
{
    public class GetGameInfoHistoryResponseView
    {
        public List<StepGetGameInfoHistoryResponseViewItem> Steps { get; set; }
    }

    public class StepGetGameInfoHistoryResponseViewItem
    {
        public List<PlayerInfoGetGameInfoHistoryResponseViewItem> PlayerInfo { get; set; }
    }

    public class PlayerInfoGetGameInfoHistoryResponseViewItem
    {
        public string Name { get; set; }
        public int Suit { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
    }
}
