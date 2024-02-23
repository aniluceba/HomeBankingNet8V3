using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);
        Client FindByEmail(string email);

    }
}