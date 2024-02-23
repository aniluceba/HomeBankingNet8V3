using HomeBankingNet8V3.Models;
using System.Collections.Generic;

namespace HomeBankingNet8V3.Repositories
{
    public interface ICardRepository
    {
        void Save(Card card);
        Card FindById(long id);
        IEnumerable<Card> GetAllCards();
    }
}