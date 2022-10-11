using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.StockMovements
{
    public class MovementManager : IDomainService, ITransientDependency
    {
        private readonly IRepository<StockMovement, int> _stockMovementRepository;
        public MovementManager(IRepository<StockMovement, int> stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }
        public StockMovement GetStockMovement(long id)
        {
            var repo = _stockMovementRepository.GetAll()
                .Where(p => p.Id == id);

            if (repo.Count() == 0)
                return null;
            return repo.First();
        }
        public void CreateStockMovement(StockMovement stockMovement)
        {
            _stockMovementRepository.Insert(stockMovement);


        }
        public IQueryable<StockMovement> GetStockMovementList()
        {
            var producttList = _stockMovementRepository.GetAll();

            return producttList
                .Include(p => p.Storage)
                .Include(p => p.Product);

        }

        public List<StockMovement> GetStockMovementListByStorageId(int storageId)
        {
            var stockmovementList = _stockMovementRepository.GetAll().Where(p => p.Storage.Id == storageId);


            return stockmovementList.ToList();

        }
        public List<StockMovement> GetStockMovementListByStorageProductId(int storageId, int productId)
        {
            var stockmovementList = _stockMovementRepository.GetAll().Where(p => p.Storage.Id == storageId && p.Product.Id == productId);

            return stockmovementList.ToList();

        }



        public void DeleteStockMovement(StockMovement entity)
        {
            _stockMovementRepository.Delete(entity);
        }

        public IQueryable<StockMovement> GetProductList()
        {
            var productList = _stockMovementRepository.GetAll();

            return productList;

        }

    }
}