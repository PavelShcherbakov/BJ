using BJ.ViewModels.HistoryView;
using System.Threading.Tasks;

namespace BJ.BLL.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<GetAllGamesHistoryResponseView> GetAllGames(string userId);
        Task<GetGameInfoHistoryResponseView> GetGameInfo(string userId, GetGameInfoHistoryView model);

    }
}
