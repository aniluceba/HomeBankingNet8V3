using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;

namespace HomeBankingNet8v3.Repositories
{
    public class ClientLoanRepository : RepositoryBase<ClientLoan>, IClientLoanRepository
    {
        public ClientLoanRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public void Save(ClientLoan clientLoan)
        {
            Create(clientLoan);
            SaveChanges();
        }
    }

   
}