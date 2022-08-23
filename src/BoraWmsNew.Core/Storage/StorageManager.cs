using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Storage
{
    public class StorageManager : IDomainService, ITransientDependency
    {


        private readonly IRepository<Storage, int> _storageRepository;
        public StorageManager(IRepository<Storage, int> storageRepository)
        {
            _storageRepository = storageRepository;
        }
        public Storage GetStorage(long id)
        {
            var repo = _storageRepository.GetAll()
                .Where(p => p.Id == id);

            if (repo.Count() == 0)
                return null;
            return repo.First();
        }
        public void CreateStorage(Storage storage)
        {
            _storageRepository.Insert(storage);

        }
        public void UpdateStorage(Storage storage)
        {
            _storageRepository.Update(storage);

        }
        public void DeleteStorage(Storage entity)
        {
            _storageRepository.Delete(entity);
        }
        public IQueryable<Storage> GetStorageList()
        {
            var storageList = _storageRepository.GetAll();

            return storageList;

        }
    }

}
