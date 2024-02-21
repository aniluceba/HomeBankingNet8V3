using HomeBankingNet8V3.Models;
using System.Collections.Generic;

namespace HomeBankingNet8V3.Repositories.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);
    }
}
