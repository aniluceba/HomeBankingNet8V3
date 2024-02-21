using HomeBankingNet8V3.Models;
using System.Collections.Generic;

namespace HomeBankingNet8V3.Repositories
{
    public interface ITransactionRepository
    {
        void Save(Transaction transaction);
        Transaction FindById(long id);
        IEnumerable<Transaction> GetAllTransactions();
    }
}