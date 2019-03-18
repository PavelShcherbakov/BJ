using BJ.BLL.Commons;
using BJ.DAL.Interfaces;
using BJ.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BJ.BLL.Configrutions.DbInitiolaze
{
    static class BotsData
    {
        private static string[] _botsName = new string[] { "Olivia","Amelia","Isla","Emily","Ava","Lily","Mia", "Sofia", "Isabella","Grace",
                                                           "Oliver","Harry","Jack","George","Noah","Charlie", "Jacob","Alfie","Freddie","Oscar"};

        public static void Initialize(IBotRepository botRepository)
        {
            var totalCount = botRepository.GetTotalCount();
            if (totalCount == 0)
            {
                List<Bot> bots = new List<Bot>(Constants.GameSettings.MaxCountBots);
                for (int i = 0; i < Constants.GameSettings.MaxCountBots; i++)
                {
                    var bot = new Bot()
                    {
                        Name = _botsName[i % _botsName.Count()]
                    };
                    bots.Add(bot);
                }
                botRepository.AddRange(bots);
            }
        }
    }
}
