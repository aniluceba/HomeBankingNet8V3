using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        Account FindById(long id);
    }
}