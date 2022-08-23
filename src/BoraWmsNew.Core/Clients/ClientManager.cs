using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Clients
{
    public class ClientManager : IDomainService, ITransientDependency
    {
        private readonly IRepository<Client, int> _clientRepository;
        public ClientManager(IRepository<Client, int> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client GetClient(long id)
        {
            var repo = _clientRepository.GetAll()
                .Where(p => p.Id == id);

            if (repo.Count() == 0)
                return null;
            return repo.First();
        }
        public void CreateClient(Client client)
        {
            _clientRepository.Insert(client);

        }

        public void UpdateClient(Client client)
        {
            _clientRepository.Update(client);

        }

        public void Delete(object client)
        {
            _clientRepository.Delete((Client)client);
        }
        public void DeleteClient(Client entity)
        {
            _clientRepository.Delete(entity);
        }
        public IQueryable<Client> GetClientList()
        {
            var clientList = _clientRepository.GetAll();

            return clientList;

        }
    }

}
