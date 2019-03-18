using System.Collections.Generic;

namespace BJ.ViewModels.HistoryView
{
    public class GetGameInfoHistoryResponseView
    {
        public List<StepGetGameInfoHistoryResponseViewItem> Steps { get; set; }
        public List<PlayersSummaryGetGameInfoHistoryResponseViewItem> Summary { get; set; }
    }

    public class StepGetGameInfoHistoryResponseViewItem
    {
        public List<PlayerInfoGetGameInfoHistoryResponseViewItem> PlayerInfo { get; set; }
    }

    public class PlayerInfoGetGameInfoHistoryResponseViewItem
    {
        public string Name { get; set; }
        public CardGetGameInfoHistoryResponseView Card { get; set; }

    }

    public class CardGetGameInfoHistoryResponseView
    {
        public SuitGetGameInfoHistoryResponseView Suit { get; set; }
        public RankGetGameInfoHistoryResponseView Rank { get; set; }
    }

    public class SuitGetGameInfoHistoryResponseView
    {
        public int Suit { get; set; }
        public string SuitAsString { get; set; }
    }

    public class RankGetGameInfoHistoryResponseView
    {
        public int Rank { get; set; }
        public string RankAsString { get; set; }
    }

    public class PlayersSummaryGetGameInfoHistoryResponseViewItem
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public StateGetGameInfoHistoryResponseView State { get; set; }
    }

    public class StateGetGameInfoHistoryResponseView
    {
        public int State { get; set; }
        public string StateAsString { get; set; }
    }

}
