using BJ.ViewModels.GameViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.BLL.Services.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResponseView> StartGame(string userId, StartGameView model);

        Task<GetStateGameResponseView> GetState(string userId);

        Task<GetCardGameResponseView> GetCard(string userId);

        Task<EndGameResponseView> EndGame(string userId);

        Task<HasActiveGameGameResponseView> HasActiveGame(string userId);
    }
}
