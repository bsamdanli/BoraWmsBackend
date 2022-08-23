using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using BoraWmsNew.Authorization;
using BoraWmsNew.Clients;
using BoraWmsNew.Products;
using BoraWmsNew.Receiving.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Receiving
{
    [AbpAuthorize(PermissionNames.Pages_Receiving)]
    public class ReceivingAppService : BoraWmsNewAppServiceBase
    {
        private readonly ReceivingManager _receivingManager;
        private readonly ClientManager _clientManager;
        private readonly ProductManager _productManager;

        private readonly IRepository<Document, int> _repository;
        public ReceivingAppService(ReceivingManager receivingManager, ClientManager clientManager, ProductManager productManager)
        {
            _receivingManager = receivingManager;
            _clientManager = clientManager;
            _productManager = productManager;

        }

        public ReceivingDto CreateReceivingDocument(ReceivingDto receivingDto)
        {
            Client client = _clientManager.GetClient(receivingDto.ClientId);
            if (client == null)
                throw new UserFriendlyException("Seçilen cari bulunamadı.");
            if (receivingDto.DocumentNo == "0")
                throw new UserFriendlyException("Döküman numarası 0 olamaz.");
            //Document DocumentNo = _receivingManager.GetDocument(receivingDto.DocumentNo);
            //if (DocumentNo == receivingDto.DocumentNo)
            //    throw new UserFriendlyException("Seçilen Evrak Numarası Zaten Mevcut.");

            Document document = new()
            {
                Client = client,
                DocumentNo = receivingDto.DocumentNo,
                CreationTime = DateTime.Now,
                DocumentDate = receivingDto.DocumentDate,

            };
            _receivingManager.CreateDocument(document);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ReceivingDto>(document);

        }
        public void CreateReceivingDocumentDetail(DocumentDetailDto documentDetailDto)
        {
            Client client = _clientManager.GetClient(documentDetailDto.ClientId);
            if (client == null)
                throw new UserFriendlyException("Seçilen cari bulunamadı.");
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


        }

        public ReceivingDto GetDocument(long id)
        {
            Document document = _receivingManager.GetDocument(id);
            return ObjectMapper.Map<ReceivingDto>(document);
        }

        public List<ReceivingDto> GetDocumentAll()
        {
            return ObjectMapper.Map<List<ReceivingDto>>(_receivingManager.GetDocumentList());
        }


        public List<DocumentDetailDto> GetDocumentDetailAll(int DocumentId)
        {
            List<DocumentDetail> documentDetails = _receivingManager.GetDocumentDetailListByDocumentId(DocumentId).ToList();
            var testReturn = ObjectMapper.Map<List<DocumentDetailDto>>(documentDetails);
            foreach (var documentDetail in testReturn)
            {
                Product product = _productManager.GetProduct(documentDetail.ProductId);
                documentDetail.ProductName = product.Name;
            }
            return testReturn;
        }

        public void DeleteDocument(int documentId)
        {
            var document = _receivingManager.GetDocument(documentId);
            if (document != null)
            {
                _receivingManager.DeleteDocument(document);
            }
            else
            {
                throw new UserFriendlyException("Olmadı.");

            }
        }
        public void DeleteDocumentDetail(int documentDetailId)
        {
            var documentDetail = _receivingManager.GetDocumentDetail(documentDetailId);
            if (documentDetail != null)
            {
                _receivingManager.DeleteDocumentDetail(documentDetail);
            }
            else
            {
                throw new UserFriendlyException("Olmadı.");

            }
        }

    }

}
