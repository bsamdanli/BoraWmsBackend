using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using BoraWmsNew.Authorization;

using BoraWmsNew.Clients;
using BoraWmsNew.Clients.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Clients
{
    [AbpAuthorize(PermissionNames.Pages_Client)]
    public class ClientsAppService : BoraWmsNewAppServiceBase
    {
        private ClientManager _clientManager;
        public ClientsAppService(
            ClientManager clientManager)
        {
            _clientManager = clientManager;
        }
        public ClientDto GetClient(long id)
        {
            Client client = _clientManager.GetClient(id);
            ClientDto retObj = ObjectMapper.Map<ClientDto>(client);

            return retObj;
        }
        public List<ClientDto> GetClientAll()
        {
            List<Client> client = _clientManager.GetClientList().ToList();
            List<ClientDto> retObj = ObjectMapper.Map<List<ClientDto>>(client);
            return retObj;

        }

        public ClientDto CreateClient(ClientDto clientDto)
        {
            if (_clientManager.GetClientList().Where(o => o.Name == clientDto.Name).Count() > 0)
                throw new UserFriendlyException("Oluşturmak İstediğiniz Müşteri Zaten Mevcut.");

            Client client = new()
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                Address = clientDto.Address,
                TaxNumber = clientDto.TaxNumber,
                TaxOffice = clientDto.TaxOffice,
                PhoneNumber = clientDto.PhoneNumber,
                IsActive = clientDto.IsActive
            };
            _clientManager.CreateClient(client);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ClientDto>(client);


        }
        //pagedresultdto
        public PagedResultDto<ClientDto> GetClientListPaged(PagedClientResultRequestDto input)
        {
            var repo = _clientManager.GetClientList().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Name.Contains(input.Keyword))
            .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
            var count = repo.Count();
            repo = repo.OrderBy(p => p.Name).PageBy(input);
            return new PagedResultDto<ClientDto>()
            {
                Items = ObjectMapper.Map<List<ClientDto>>(repo),
                TotalCount = count
            };
        }

        public ClientDto UpdateClient(ClientDto clientDto)
        {
            var client = _clientManager.GetClient(clientDto.Id);

            var clientOld =  _clientManager.GetClientList().Where(c=>c.Name== clientDto.Name).FirstOrDefault();

            if (client != null && clientOld.Id != client.Id)
            {
                throw new UserFriendlyException("Girilen Müşteri İsmine Sahip bir Müşteri Mevcut.");
            }

            

            client.Name = String.IsNullOrEmpty(clientDto.Name) ? client.Name : clientDto.Name;
            client.Email = String.IsNullOrEmpty(clientDto.Email) ? clientDto.Email : clientDto.Email;
            client.Address = String.IsNullOrEmpty(client.Address) ? clientDto.Address : clientDto.Address;
            client.TaxNumber = String.IsNullOrEmpty(clientDto.TaxNumber) ? clientDto.TaxNumber : clientDto.TaxNumber;
            client.TaxOffice = String.IsNullOrEmpty(clientDto.TaxOffice) ? clientDto.TaxOffice : clientDto.TaxOffice;
            client.PhoneNumber = String.IsNullOrEmpty(clientDto.PhoneNumber) ? clientDto.PhoneNumber : clientDto.PhoneNumber;
            client.IsActive= clientDto.IsActive;

            _clientManager.UpdateClient(client);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ClientDto>(client);


        }
        public void DeleteClient(int clientId)
        {
            var client = _clientManager.GetClient(clientId);
            if (client != null)
            {
                _clientManager.DeleteClient(client);
            }
            else
            {
                throw new UserFriendlyException("Başaramadık.");

            }
        }

        //protected override IQueryable<Client> CreateFilteredQuery(PagedClientResultRequestDto input)
        //{
        //    return Repository.GetAll()
        //        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Name.Contains(input.Keyword))
        //        .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        //}

        //public ClientDto DeleteClient(ClientDto clientDto)
        //{
        //    C

        //    var client = await _clientManager.GetById(input.Id);
        //    await _clientManager.Delete(client);
        //}
    }

}
