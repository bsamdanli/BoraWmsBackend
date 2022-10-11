using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using BoraWmsNew.Authorization;
using BoraWmsNew.Storages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoraWmsNew.Storages
{
    [AbpAuthorize(PermissionNames.Pages_Storage)]
    public class StorageAppService : BoraWmsNewAppServiceBase
    {
        private StorageManager _storageManager;

        private readonly IRepository<Storage, int> _repository;
        public StorageAppService(
            StorageManager storageManager)
        {
            _storageManager = storageManager;
        }
        public StorageDto GetStorage(long id)
        {
            Storage storage = _storageManager.GetStorage(id);
            return ObjectMapper.Map<StorageDto>(storage);
        }
        public List<StorageDto> GetStorageAll()
        {
            List<Storage> storage = _storageManager.GetStorageList().ToList();
            return ObjectMapper.Map<List<StorageDto>>(storage);
        }
        public StorageDto CreateStorage(StorageDto storageDto)
        {

            if (_storageManager.GetStorageList().Where(o => o.StorageCode == storageDto.StorageCode).Count() > 0)
                throw new UserFriendlyException(storageDto.StorageCode + " Kodlu Oluşturmak İstediğiniz Ambar Zaten Mevcut.");

            Storage storage = new()
            {
                Name = storageDto.Name,
                StorageCode = storageDto.StorageCode,
                StorageType = storageDto.StorageType,
            };
            _storageManager.CreateStorage(storage);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<StorageDto>(storage);
        }

        public PagedResultDto<StorageDto> GetStorageListPaged(PagedStorageResultRequestDto input)
        {
            var repo = _storageManager.GetStorageList().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Name.Contains(input.Keyword));
            var count = repo.Count();
            repo = repo.OrderBy(p => p.Name).PageBy(input);
            return new PagedResultDto<StorageDto>()
            {
                Items = ObjectMapper.Map<List<StorageDto>>(repo),
                TotalCount = count
            };
        }

        public StorageDto UpdateStorage(StorageDto storageDto)
        {
            var storage = _storageManager.GetStorage(storageDto.Id);

            var storageOld = _storageManager.GetStorageList().Where(c => c.StorageCode == storageDto.StorageCode).FirstOrDefault();

            if (storage != null && storageOld.Id != storage.Id)
            {
                throw new UserFriendlyException(storageDto.StorageCode + " Kodlu Ambar Zaten Mevcut.");
            }

            storage.Name = String.IsNullOrEmpty(storageDto.Name) ? storageDto.Name : storageDto.Name;
            storage.StorageCode = String.IsNullOrEmpty(storageDto.StorageCode) ? storageDto.StorageCode : storageDto.StorageCode;
            storage.StorageType = String.IsNullOrEmpty(storageDto.StorageType) ? storageDto.StorageType : storageDto.StorageType;



            _storageManager.UpdateStorage(storage);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<StorageDto>(storage);
        }
        public void DeleteStorage(int storageId)
        {
            var storage = _storageManager.GetStorage(storageId);
            if (storage != null)
            {
                _storageManager.DeleteStorage(storage);
            }
            else
            {
                throw new UserFriendlyException("Yapamadık.");

            }
        }
    }

}
