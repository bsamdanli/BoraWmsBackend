using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using BoraWmsNew.Authorization;
using BoraWmsNew.Movement.Dto;
using BoraWmsNew.Products;
using BoraWmsNew.StockMovements;
using BoraWmsNew.Stocks;
using BoraWmsNew.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;


namespace BoraWmsNew.MovementAppService
{
    [AbpAuthorize(PermissionNames.Pages_StockMovement)]
    public class MovementAppService : BoraWmsNewAppServiceBase
    {
        private readonly MovementManager _movementManager;
        private readonly ProductManager _productManager;
        private readonly StorageManager _storageManager;

        private readonly IRepository<StockMovement, int> _repository;
        public MovementAppService(
            MovementManager movementManager,
            ProductManager productManager,
            StorageManager storageManager)
        {
            _movementManager = movementManager;
            _productManager = productManager;
            _storageManager = storageManager;
        }
        public StockMovementDto GetStockMovement(long id)
        {
            StockMovement stockmovement = _movementManager.GetStockMovement(id);
            return ObjectMapper.Map<StockMovementDto>(stockmovement);
        }

        public PagedResultDto<StockMovementReportDto> GetStockMovementListPaged(PagedMovementResultRequestDto input)
        {
            var repo = _movementManager.GetStockMovementList().Include(x=>x.Storage).WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Storage.Name.Contains(input.Keyword) || x.Storage.Name.Contains(input.Keyword));
            var count = repo.Count();
            repo = repo.PageBy(input);
            return new PagedResultDto<StockMovementReportDto>()
            {
                Items = ObjectMapper.Map<List<StockMovementReportDto>>(repo),
                TotalCount = count
            };
        }

        public List<StockMovementDto> GetStockMovementAll()
        {
            List<StockMovement> stockMovements = _movementManager.GetStockMovementList().ToList();

            return ObjectMapper.Map<List<StockMovementDto>>(stockMovements);
        }

        //public StockMovementDto CreateStockMovementBatch ( List<StockMovementDto> stockMovementDtos)
        //{

        //}

        public StockMovementDto CreateStockMovement(StockMovementDto stockMovementDto)
        {
            Product product = _productManager.GetProduct(stockMovementDto.ProductId);
            Storage storage = _storageManager.GetStorage(stockMovementDto.StorageId);

            StockMovement stockMovement = new()
            {


                Quantity = stockMovementDto.Quantity,
                Product = product,
                Type = stockMovementDto.Type,
                Storage = storage,
                CreationTime = DateTime.Now,



            };

            if (stockMovementDto.Type == MovementType.In)
            {
                product.Quantity += stockMovementDto.Quantity;
            }

            if (stockMovementDto.Type == MovementType.Out)
            {
                product.Quantity -= stockMovementDto.Quantity;
            }
            if (product.Quantity < stockMovementDto.Quantity)
            {
                throw new UserFriendlyException("Çıkış Ambarında yetersiz miktar");
            }



            _movementManager.CreateStockMovement(stockMovement);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<StockMovementDto>(stockMovement);
            //return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.AsDto());
        }
        public void DeleteStockMovement(int stockMovementId)
        {
            var stockMovement = _movementManager.GetStockMovement(stockMovementId);
            if (stockMovement != null)
            {
                _movementManager.DeleteStockMovement(stockMovement);
            }
            else
            {
                throw new UserFriendlyException("Olmadı.");

            }

        }
        public void CreateTransferMovement(TransferMovementDto transferMovementDto)
        {
            Product product = _productManager.GetProduct(transferMovementDto.ProductId);
            Storage storageIn = _storageManager.GetStorage(transferMovementDto.StorageInId);
            Storage storageOut = _storageManager.GetStorage(transferMovementDto.StorageOutId);
            List<StockMovement> stockMovements = _movementManager.GetStockMovementListByStorageProductId(storageOut.Id, product.Id);

            int sum = 0;
            foreach (StockMovement stockMovement in stockMovements)
            {
                if (stockMovement.Type == MovementType.In)
                {
                    sum = (sum + stockMovement.Quantity);
                }
                if (stockMovement.Type == MovementType.Out)
                {
                    sum = (sum - stockMovement.Quantity);
                }
            }

            if (sum < transferMovementDto.Quantity)
                throw new UserFriendlyException("Çıkış Ambarında yetersiz miktar");

            StockMovement stockMovementOut = new()
            {
                Quantity = transferMovementDto.Quantity,
                Product = product,
                Type = MovementType.Out,
                Storage = storageOut
            };
            _movementManager.CreateStockMovement(stockMovementOut);
            CurrentUnitOfWork.SaveChanges();
            StockMovement stockMovementIn = new()
            {
                Quantity = transferMovementDto.Quantity,
                Product = product,
                Type = MovementType.In,
                Storage = storageIn
            };

            _movementManager.CreateStockMovement(stockMovementIn);
            CurrentUnitOfWork.SaveChanges();



        }

        public List<StockMovementReportDto> GetStockReportAll()
        {
            List<StockMovement> stockReports = _movementManager.GetStockMovementList().Where(p => p.Product != null || p.Storage != null).ToList();
            var testReturn = ObjectMapper.Map<List<StockMovementReportDto>>(stockReports);
            //var repoToreturn = ObjectMapper.Map<List<StockMovementDto>>(stockReports);
            //var testMap = new List<StockReportDto>();
            //foreach (var stock in stockReports)
            //{
            //    var test = new StockReportDto()
            //    {

            //        Id = stock.Id,
            //        CreationTime = stock.CreationTime,
            //        ProductCode = stock.Product.Name,
            //        StorageCode = stock.Storage.Name,
            //        Quantity = stock.Quantity,
            //        Type = stock.Type
            //    };
            //    testMap.Add(test);
            //}
            return testReturn;

        }

        public List<StockDto> GetStockQuantity(int storageId, int productId)
        {
            List<StockMovement> stockMovements = _movementManager.GetStockMovementListByStorageProductId(storageId, productId);
            List<StockDto> result = new List<StockDto>();

            int sum = 0;
            foreach (StockMovement stockMovement in stockMovements)
            {
                if (stockMovement.Type == MovementType.In)
                {
                    sum = (sum + stockMovement.Quantity);
                }
                if (stockMovement.Type == MovementType.Out)
                {
                    sum = (sum - stockMovement.Quantity);
                }
            }

            var storage = _storageManager.GetStorage(storageId);
            var product = _productManager.GetProduct(productId);
            if (storage == null)
            {
                throw new UserFriendlyException("Ambar bulunamadı.");
            }
            if (product == null)
            {
                throw new UserFriendlyException("Ürün bulunamadı.");
            }
            StockDto stockDto = new StockDto()
            {
                StorageName = storage.Name,
                ProductName = product.Name,
                StockQuantity = sum

            };

            result.Add(stockDto);

            return result;


        }










    }
}
