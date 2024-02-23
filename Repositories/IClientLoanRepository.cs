using HomeBankingNet8V3.Models;


namespace HomeBankingNet8V3.Repositories
{
    public interface IClientLoanRepository
    {
        public void Save(ClientLoan clientLoan);
    }
}