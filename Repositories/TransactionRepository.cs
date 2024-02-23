using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;


namespace HomeBankingNet8V3.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {

        }
        public Transaction FindByNumber(long Id)
        {
            return FindByCondition(transaction => transaction.Id == Id).FirstOrDefault();
        }

        public void Save(Transaction transaction)
        {
            Create(transaction);
            SaveChanges();
        }

        public void Save(System.Transactions.Transaction transaction)
        {
            throw new NotImplementedException();
        }

        System.Transactions.Transaction ITransactionRepository.FindByNumber(long Id)
        {
            throw new NotImplementedException();
        }
    }
}