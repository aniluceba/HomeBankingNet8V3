using HomeBankingNet8V3.Models;
using System.Collections.Generic;

namespace HomeBankingNet8V3.Repositories
{
    public interface ILoanRepository
    {
        public IEnumerable<Loan> GetAllLoans();
        public Loan FindById(long id);
    }
}
