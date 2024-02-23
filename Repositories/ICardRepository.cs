using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public interface ICardRepository
    {
        void save(Card card);

        public bool ValidateCard(long ClientId, CardType Type, CardColor Color);

        IEnumerable<Card> GetAllCardsFrom(string Email);
    }
}
