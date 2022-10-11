using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using BoraWmsNew.Authorization;
using BoraWmsNew.Clients;
using BoraWmsNew.Products;
using BoraWmsNew.Receiving.Dto;
using BoraWmsNew.StockMovements;
using BoraWmsNew.Storages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BoraWmsNew.Receiving
{
    [AbpAuthorize(PermissionNames.Pages_Receiving)]
    public class ReceivingAppService : BoraWmsNewAppServiceBase
    {
        private readonly ReceivingManager _receivingManager;
        private readonly ClientManager _clientManager;
        private readonly ProductManager _productManager;
        private readonly MovementManager _movementManager;
        private readonly StorageManager _storageManager;
        

        private readonly IRepository<Document, int> _repository;
        public ReceivingAppService(ReceivingManager receivingManager, ClientManager clientManager, ProductManager productManager, MovementManager movementManager, StorageManager storageManager)
        {
            _receivingManager = receivingManager;
            _clientManager = clientManager;
            _productManager = productManager;
            _movementManager = movementManager;
            _storageManager = storageManager;

        }

        public ReceivingDto CreateReceivingDocument(ReceivingDto receivingDto)
        {
            Client client = _clientManager.GetClient(receivingDto.ClientId);
            if (client == null)
                throw new UserFriendlyException("Seçilen cari bulunamadı.");
            if (receivingDto.DocumentNo == "0")
                throw new UserFriendlyException("Döküman numarası 0 olamaz.");
            
            Document document = new()
            {
                Client = client,
                DocumentNo = receivingDto.DocumentNo,
                CreationTime = DateTime.Now,
                DocumentDate = receivingDto.DocumentDate,
                IsCompleted= false

            };
            _receivingManager.CreateDocument(document);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ReceivingDto>(document);

        }
        public void CreateReceivingDocumentDetail(DocumentDetailDto documentDetailDto)
        {
            //Client client = _clientManager.GetClient(documentDetailDto.ClientId);
            //if (client == null)
            //    throw new UserFriendlyException("Seçilen cari bulunamadı.");
            Product product = _productManager.GetProduct(documentDetailDto.ProductId);
            //if (receivingDto.DocumentNo == "0")
            //    throw new UserFriendlyException("Döküman numarası 0 olamaz.");

            DocumentDetail documentDetail = new()
            {
                Quantity = documentDetailDto.Quantity,
                DocumentId = documentDetailDto.DocumentId,
                Product = product,
                CreationTime = DateTime.Now,
            };
            _receivingManager.CreateDocumentDetail(documentDetail);
            CurrentUnitOfWork.SaveChanges();
            
            var storage = _storageManager.GetStorage(1);
            _movementManager.CreateStockMovement(new StockMovement()
            {
                Quantity = documentDetailDto.Quantity,
                Product = product,
                Type = MovementType.In,
                Storage = storage
            });


        }

        public ReceivingDto GetDocument(long id)
        {
            Document document = _receivingManager.GetDocument(id);
            return ObjectMapper.Map<ReceivingDto>(document);
        }

        public PagedResultDto<ReceivingDto> GetDocumentAll(PagedDocumentResultRequestDto input)
        {
            //return ObjectMapper.Map<List<ReceivingDto>>(_receivingManager.GetDocumentList());
            var repo = _receivingManager.GetDocumentList().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.DocumentNo.Contains(input.Keyword));
            var count = repo.Count();
            var repoNew = repo.OrderBy(p => p.DocumentNo).AsQueryable().PageBy(input).ToList();
            return new PagedResultDto<ReceivingDto>()
            {
                Items = ObjectMapper.Map<List<ReceivingDto>>(repoNew),
                TotalCount = count
            };
        }


        public List<DocumentDetailDto> GetDocumentDetailAll(int DocumentId)
        {
            List<DocumentDetail> documentDetails = _receivingManager.GetDocumentDetailListByDocumentId(DocumentId).ToList();
            var testReturn = ObjectMapper.Map<List<DocumentDetailDto>>(documentDetails);
            foreach (var documentDetail in testReturn)
            {
                Product product = _productManager.GetProduct(documentDetail.ProductId);
                // documentDetail.ProductName = product.Name;
            }
            return testReturn;
        }

        

        public PagedResultDto<DocumentDetailDto> GetDocumentDetailListPaged(PagedDocumentDetailResultRequestDto input, int DocumentId)
        {
            var repo = _receivingManager.GetDocumentDetailListByDocumentId(DocumentId).WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Product.Name.Contains(input.Keyword) || x.Product.Name.Contains(input.Keyword));
            var count = repo.Count();
            var repoNew = repo.OrderBy(p => p.Product.Name).AsQueryable().PageBy(input).ToList();
            return new PagedResultDto<DocumentDetailDto>()
            {
                Items = ObjectMapper.Map<List<DocumentDetailDto>>(repoNew),
                TotalCount = count
            };
        }

        public void CompleteDocumentDetail(int detailId)
        {
           
            var detail = _receivingManager.GetDocumentDetail(detailId);
            Product product = _productManager.GetProduct(detail.ProductId);

            var storageEntrance = _storageManager.GetStorage(1);
            var storageMain = _storageManager.GetStorage(2);

            _movementManager.CreateStockMovement(new StockMovement()
            {
                Quantity = detail.Quantity,
                Product = product,
                Type = MovementType.Out,
                Storage = storageEntrance
            });

            _movementManager.CreateStockMovement(new StockMovement()
            {
                Quantity = detail.Quantity,
                Product = product,
                Type = MovementType.In,
                Storage = storageMain
            });

            detail.IsCompleted = true;
        }

        public void CompleteDocument(int id)
        {
            var document = _receivingManager.GetDocument(id);
            document.IsCompleted = true;

        }

        public void DeleteDocument(int documentId)
        {
            var document = _receivingManager.GetDocument(documentId);
            var documentDetails = _receivingManager.GetDocumentDetailListByDocumentId(documentId);
            

            var storageEntrance = _storageManager.GetStorage(1);
            var storageMain = _storageManager.GetStorage(2);
            foreach (var detail in documentDetails)
            {
                Product product = _productManager.GetProduct(detail.ProductId);
                if (!detail.IsCompleted)
                {
                    _movementManager.CreateStockMovement(new StockMovement()
                    {
                        Quantity = detail.Quantity,
                        Product = product,
                        Type = MovementType.Out,
                        Storage = storageEntrance
                    });
                }
                else
                {
                    _movementManager.CreateStockMovement(new StockMovement()
                    {
                        Quantity = detail.Quantity,
                        Product = product,
                        Type = MovementType.Out,
                        Storage = storageMain
                    });

                    _movementManager.CreateStockMovement(new StockMovement()
                    {
                        Quantity = detail.Quantity,
                        Product = product,
                        Type = MovementType.In,
                        Storage = storageEntrance
                    });

                    _movementManager.CreateStockMovement(new StockMovement()
                    {
                        Quantity = detail.Quantity,
                        Product = product,
                        Type = MovementType.Out,
                        Storage = storageEntrance
                    });
                }
            }

            _receivingManager.DeleteDocument(document);
        }
        public void DeleteDocumentDetail(int documentDetailId)
        {
            var detail = _receivingManager.GetDocumentDetail(documentDetailId);

            Product product = _productManager.GetProduct(detail.ProductId);

            var storageEntrance = _storageManager.GetStorage(1);
            var storageMain = _storageManager.GetStorage(2);

            if(!detail.IsCompleted)
            {
                _movementManager.CreateStockMovement(new StockMovement()
                {
                    Quantity = detail.Quantity,
                    Product = product,
                    Type = MovementType.Out,
                    Storage = storageEntrance
                });
            } else
            {
                _movementManager.CreateStockMovement(new StockMovement()
                {
                    Quantity = detail.Quantity,
                    Product = product,
                    Type = MovementType.Out,
                    Storage = storageMain
                });

                _movementManager.CreateStockMovement(new StockMovement()
                {
                    Quantity = detail.Quantity,
                    Product = product,
                    Type = MovementType.In,
                    Storage = storageEntrance
                });
            
                _movementManager.CreateStockMovement(new StockMovement()
                {
                    Quantity = detail.Quantity,
                    Product = product,
                    Type = MovementType.Out,
                    Storage = storageEntrance
                });
            }

            detail.IsDeleted = true;
        }

    }

}
