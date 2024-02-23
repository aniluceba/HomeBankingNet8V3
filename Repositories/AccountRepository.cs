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
                 .Include(Account => Account.Transaction).FirstOrDefault();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
               .Include(Account => Account.Transaction).ToList();
        }
    }
}