using System;
using System.Collections.Generic;

namespace BJ.ViewModels.HistoryView
{
    public class GetAllGamesHistoryResponseView
    {
        public List<GameGetAllGamesHistoryResponseViewItem> Games { get; set; }
    }

    public class GameGetAllGamesHistoryResponseViewItem
    {
        public Guid GameId { get; set; }
        public DateTime CreationDate { get; set; }
        public int NumberOfPlayers { get; set; }
        public ResultGetAllGamesHistoryResponseViewItem Result { get; set; }
    }

    public class ResultGetAllGamesHistoryResponseViewItem
    {
        public int State { get; set; }
        public string StateAsString { get; set; }
    }
}
