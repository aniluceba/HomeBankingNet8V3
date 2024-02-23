using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingNet8V3.Repositories
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository(HomeBankingContext repositoryContext) : base(repositoryContext) { }

        public Loan FindById(long id)
        {
            return FindByCondition(Loan => Loan.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            return FindAll();
        }

    }
}