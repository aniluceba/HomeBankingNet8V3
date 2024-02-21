using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingNet8V3.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public Transaction FindById(long id)
        {
            return FindByCondition(transaction => transaction.Id == id)
                   .FirstOrDefault();
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return FindAll()
                   .ToList();
        }
        public void Save(Transaction transaction)
        {
            Create(transaction);
            SaveChanges();
        }
    }
}