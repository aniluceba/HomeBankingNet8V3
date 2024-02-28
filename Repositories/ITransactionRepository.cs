using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public interface ITransactionRepository
    {
        void Save(Transaction transaction);
        Transaction FindByNumber(long Id);
    }
}
