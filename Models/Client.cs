using System.Collections.Generic;

namespace HomeBankingNet8V3.Models
{
    public class Client
    {
        internal readonly object Loan;

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<ClientLoan> ClientLoans { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}