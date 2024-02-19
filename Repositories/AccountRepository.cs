using HomeBankingNet8V3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingNet8V3.Repositories
{
    public class AccountRepository : RepositoryBase<Account>

    {

        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)

        {

        }

        public Account FindById(long id)

        {

            return FindByCondition(account => account.Id == id)

            .Include(account => account.Transactions)

            .FirstOrDefault();

        }



        public IEnumerable<Account> GetAllAccounts()

        {

            return FindAll()

            .Include(account => account.Transactions)

            .ToList();

        }



        public void Save(Account account)

        {

            Create(account);

            SaveChanges();

        }



        public IEnumerable<Account> GetAccountsByClient(long clientId)

        {

            return FindByCondition(account => account.ClientId == clientId)

            .Include(account => account.Transactions)

            .ToList();

        }

    }

}
    


//public class AccountRepository:RepositoryBase<Account>
//{
//public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
//{
//}

// Supongamos que tenemos una lista de cuentas
//private List<Account> accounts;

// Constructor de la clase
//public AccountRepository()
//{
// Inicializamos la lista de cuentas
//  accounts = new List<Account>();
//}

// Implementación del método para obtener todas las cuentas
// public IEnumerable<Account> GetAllAccounts()
//{
//  return accounts;
//}

// Implementación del método para obtener una cuenta por su ID
//public Account GetAccountById(int accountId)
//{
// Supongamos que buscamos la cuenta en la lista por su ID
//  return accounts.Find(account => account.Id == accountId);
//}
//}
