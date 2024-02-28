using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingNet8V3.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {

        }
        public Account FindById(long id)
        {
            return FindByCondition(Account => Account.Id == id)
                 .Include(Account => Account.Transactions).FirstOrDefault();
        }

        public Account FindByNumber(string number)
        {
            return FindByCondition(account => account.Number.ToUpper() == number.ToUpper())
                .Include(account => account.Transactions)
                .FirstOrDefault();
        }

        public IEnumerable<Account> GetAccountsByClient(string Email)
        {
            return FindByCondition(x => x.Client.Email == Email)
                .Include(account => account.Transactions)
                .ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
               .Include(Account => Account.Transactions).ToList();
        }

        public bool HasAccountsAvailable(string Email)
        {
            return FindByCondition(account => account.Client.Email == Email).Count() < 3;
        }

        public void Save(Account account)
        {
            if (account.Id == 0)
            {
                Create(account);
            }
            else
            {
                Update(account);
            }
            SaveChanges();
        }

    }
}