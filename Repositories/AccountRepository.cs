using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingNet8V3.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public Account FindById(long id)
        {
            return FindByCondition(account => account.Id == id)
                   .Include(account => account.Transaction)
                   .FirstOrDefault();
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                   .Include(account => account.Transaction)
                   .ToList();
        }
        public void Save(Account account)
        {
            Create(account);
            SaveChanges();
        }
    }
}
