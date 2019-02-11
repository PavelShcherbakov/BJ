using BJ.BLL.Commons;
using BJ.DAL.Interfaces;
using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.BLL.Helpers
{
    public class CardHelper
    {
        static readonly Card[] cards;
        private readonly IDeckRepository _deckRepository;
        private readonly IUsersStepRepository _usersStepRepository;
        private readonly IBotsStepRepository _botsStepRepository;

        static CardHelper()
        {
            cards = new Card[Constants.NumOfCardsInDeck];
            for(int i=0; i< Enum.GetNames(typeof(Rank)).Length; i++)
            {
                for(int j=0;j< Enum.GetNames(typeof(Suit)).Length; j++)
                {
                    cards[i* Enum.GetNames(typeof(Suit)).Length + j] = new Card((Rank)i, (Suit)j);
                }
            }
        }


        public CardHelper(IDeckRepository deckRepository, IUsersStepRepository usersStepRepository, IBotsStepRepository botsStepRepository)
        {
            _deckRepository = deckRepository;
            _usersStepRepository = usersStepRepository;
            _botsStepRepository = botsStepRepository;
        }

        public async Task CreateDecksAsync(int numberOfBots,Game game)
        {
            int numOfDeck = HowManyDecks(numberOfBots);
            Deck[] decks = new Deck[numOfDeck* Constants.NumOfCardsInDeck]; 
            for(int n=0;n< numOfDeck; n++)
            {

                //Тут должно быть перемешивание, но его пока нет
                for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
                {
                    for (int j = 0; j < Enum.GetNames(typeof(Suit)).Length; j++)
                    {
                        decks[n* Constants.NumOfCardsInDeck + i* Enum.GetNames(typeof(Suit)).Length + j] = new Deck() { Game = game, Rank = (Rank)i, Suit = (Suit)j };
                    }
                }

            }
            await _deckRepository.AddRangeAsync(decks);

        }

        private int HowManyDecks(int numberOfBots)
        {
            var result = numberOfBots / 18 + 1;
            return result;
        }

        public async Task CardsDeal(Game game, User user, Bot[] bots)
        {
            UsersStep[] usersSteps = new UsersStep[2];
            BotsStep[] botsSteps = new BotsStep[2* bots.Length];

            for (int i = 0; i < 2; i++)
            {


            }
        }

    }


    public class Card
    {
        public readonly Rank rank;
        public readonly Suit suit;
        public Card(Rank rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
        }
    }
}
