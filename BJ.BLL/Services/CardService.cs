using BJ.BLL.Commons;
using BJ.DAL.Interfaces;
using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class CardService
    {

        private readonly IDeckRepository _deckRepository;
        private readonly IUsersStepRepository _usersStepRepository;
        private readonly IBotsStepRepository _botsStepRepository;

        public CardService(IDeckRepository deckRepository, IUsersStepRepository usersStepRepository, IBotsStepRepository botsStepRepository)
        {
            _deckRepository = deckRepository;
            _usersStepRepository = usersStepRepository;
            _botsStepRepository = botsStepRepository;
        }

        public async Task CreateDecksAsync(int numberOfBots,Guid gameId)
        {
            int numOfDeck = HowManyDecks(numberOfBots);
            
            for(int n=0;n< numOfDeck; n++)
            {
                await AddDeckAsync(gameId);
            }
        }

        public async Task AddDeckAsync( Guid gameId)
        {
            List<Card> decks = new List<Card>();

            for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(Suit)).Length; j++)
                {
                    decks.Add(new Card() { GameId = gameId, Rank = (Rank)i, Suit = (Suit)j });
                }
            }
            await _deckRepository.AddRangeAsync(decks);
        }

        private int HowManyDecks(int numOfBots)
        {
            var numOfDecks = (numOfBots + Constants.NumOfUserInGame) / (Constants.NumOfCardsInDeck / Constants.InitialNumOfCard)+1;
            return numOfDecks;
        }

        public async Task CardsDeal(Guid gameId, string userId, List<Bot> bots)
        {
            await RoundCardsDeal(gameId, userId, bots, Constants.InitialNumOfCard);
        }

        public async Task RoundCardsDeal(Guid gameId, string userId, List<Bot> bots, int numOfDealtCards = 1)
        {
            var usersSteps = new List<UsersStep>();
            var botsSteps = new List<BotsStep>();
            var playingBots = new List<Bot>();
            

            foreach (var bot in bots)
            {
                if (await DoesBotNeedCardAsync(bot.Id)) playingBots.Add(bot);
            }
            var numOfCards = (playingBots.Count + Constants.NumOfUserInGame) * numOfDealtCards;

            
            int countCardInDeck = _deckRepository.GetCountCards(gameId);
            if (countCardInDeck < numOfCards)
            {
                await AddDeckAsync(gameId);
            }

            List<Card> takenСards = await _deckRepository.GetRandomCardsByGameId(gameId, numOfCards) as List<Card>;

            int stepNumder = 0;
            for (int i = 0; i < numOfDealtCards; i++)
            {
                await UserGetCardAsync(takenСards, usersSteps, gameId, userId, ++stepNumder);

                foreach (var bot in playingBots)
                {
                    await BotGetCardAsync(takenСards, botsSteps, gameId, bot.Id, ++stepNumder);
                }
            }
            await _usersStepRepository.AddRangeAsync(usersSteps);
            await _botsStepRepository.AddRangeAsync(botsSteps);
        }

        private async Task UserGetCardAsync(List<Card> cards, List<UsersStep> usersSteps, Guid gameId, string userId, int stepNumder)
        {
            var card = cards.Last();

            var usersStep = new UsersStep()
            {
                GameId = gameId,
                UserId = userId,
                StepNumder = ++stepNumder,
                Rank = card.Rank,
                Suit = card.Suit
            };
            usersSteps.Add(usersStep);
            await _deckRepository.RemoveAsync(card);
            cards.Remove(card);
        }

        private async Task BotGetCardAsync(List<Card> cards, List<BotsStep> botsSteps, Guid gameId, Guid botId, int stepNumder)
        {
                var card = cards.Last();
                var botsStep = new BotsStep()
                {
                    GameId = gameId,
                    BotId = botId,
                    StepNumder = ++stepNumder,
                    Rank = card.Rank,
                    Suit = card.Suit
                };
                botsSteps.Add(botsStep);
                await _deckRepository.RemoveAsync(card);
                cards.Remove(card);
        }

        private async Task<bool> DoesBotNeedCardAsync(Guid botId)
        {
            int sum = 0;
            var botsSteps = await _botsStepRepository.FindAsync(x => x.BotId == botId) as List<BotsStep>;
            foreach(var botStep in botsSteps)
            {
                switch (botStep.Rank)
                {
                    case Rank.Six:
                        sum += 6;
                        break;
                    case Rank.Seven:
                        sum += 7;
                        break;
                    case Rank.Eight:
                        sum += 8;
                        break;
                    case Rank.Nine:
                        sum += 9;
                        break;
                    case Rank.Ten:
                        sum += 10;
                        break;
                    case Rank.Jack:
                        sum += 2;
                        break;
                    case Rank.Queen:
                        sum += 3;
                        break;
                    case Rank.King:
                        sum += 4;
                        break;
                    case Rank.Ace:
                        sum += 11;
                        break;
                }
            }
            if (sum < Constants.BotStopGame) return true;
            else return false;
        }

    }
}
