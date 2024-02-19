using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable <Account> GetAllAccounts();
        Account GetAccountById(int accountId);
        void Save(Account account);
        IEnumerable<Account> GetAccountsByClient(long clientId);
    }
}
