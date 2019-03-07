using BJ.Entities;
using System;
using System.Collections.Generic;

namespace BJ.BLL.Helpers.Interfaces
{
    public interface IDeckHelper
    {
        List<Card> CreateDeck(Guid gameId);
    }
}
