using HomeBankingNet8V3.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace HomeBankingNet8V3.Models
{
    public class DbInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client() {FirstName="Victor",LastName="Coronado",Email="vcoronado@gmail.com",Password="123456"}
                };
                context.AddRange(clients);
                context.SaveChanges();
            }
            if (!context.Account.Any())
            {
                var accountVictor = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (accountVictor != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN001", Balance = 0 },
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN002", Balance = 0 },
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN003", Balance = 0 },


                    };
        
                    context.AddRange(accounts);
                    context.SaveChanges();

                }
            }
            if (!context.Transaction.Any())

            {

                var account1 = context.Account.FirstOrDefault(c => c.Number == "VIN001");

                if (account1 != null)

                {

                    var transactions = new Transaction[]

                    {

                        new Transaction { AccountId= account1.Id, Amount = 10000, Date= DateTime.Now.AddHours(-5), Description = "Transferencia reccibida", Type = TransactionType.CREDIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -2000, Date= DateTime.Now.AddHours(-6), Description = "Compra en tienda mercado libre", Type = TransactionType.DEBIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -3000, Date= DateTime.Now.AddHours(-7), Description = "Compra en tienda xxxx", Type = TransactionType.DEBIT.ToString() },

                    };

                    foreach (Transaction transaction in transactions)

                    {

                        context.Transaction.Add(transaction);

                    }

                    context.SaveChanges();



                }

            }
            ModifyAccBalance(context);
        }
        public static void ModifyAccBalance(HomeBankingContext context)
        {
            foreach (Transaction transactions in context.Transaction.ToList())
            {
                var account = context.Account.FirstOrDefault(c => c.Id == transactions.AccountId);
                account.SetBalance(transactions.Amount);
            }
            context.SaveChanges();
        }


    }
}

