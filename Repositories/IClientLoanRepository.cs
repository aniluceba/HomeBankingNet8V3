using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public interface IClientLoanRepository
    {
        void Save(ClientLoan loan);
        ClientLoan FindById(long id);
    }
}
