using BJ.BLL.Helpers.Interfaces;
using BJ.Entities;
using BJ.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.BLL.Helpers
{
    public class DeckHelper : IDeckHelper
    {
        private readonly List<RankType> _ranks;
        private readonly List<SuitType> _suits;

        public DeckHelper()
        {
            _ranks = GetRanks();
            _suits = GetSuits();
        }

        private List<RankType> GetRanks()
        {
            var ranks = Enum.GetValues(typeof(RankType)).Cast<RankType>().ToList();
            return ranks;
        }
        private List<SuitType> GetSuits()
        {
            return Enum.GetValues(typeof(SuitType)).Cast<SuitType>().ToList();
        }

        public List<Card> CreateDeck(Guid gameId)
        {
            List<Card> deck = new List<Card>();

            for (int i = 0; i < Enum.GetNames(typeof(RankType)).Length; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(SuitType)).Length; j++)
                {
                    deck.Add(new Card() { GameId = gameId, Rank = _ranks[i], Suit = _suits[j] });
                }
            }
            return deck;
        }
    }
}
