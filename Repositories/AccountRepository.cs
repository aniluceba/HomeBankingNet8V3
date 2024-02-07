using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public class AccountRepository:RepositoryBase<Account>
    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }
        public class MyAccountRepository : IAccountRepository
        {
            // Supongamos que tenemos una lista de cuentas
            private List<Account> accounts;

            // Constructor de la clase
            public MyAccountRepository()
            {
                // Inicializamos la lista de cuentas
                accounts = new List<Account>();
            }

            // Implementación del método para obtener todas las cuentas
            public IEnumerable<Account> GetAllAccounts()
            {
                return accounts;
            }

            // Implementación del método para obtener una cuenta por su ID
            public Account GetAccountById(int accountId)
            {
                // Supongamos que buscamos la cuenta en la lista por su ID
                return accounts.Find(account => account.Id == accountId);
            }
        }


    }
}
