using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Receiving
{
    public class ReceivingManager : IDomainService, ITransientDependency

    {
        private readonly IRepository<Document> _receivingRepository;
        private readonly IRepository<DocumentDetail> _receivingDetailRepository;

        public ReceivingManager(IRepository<Document> receivingRepository, IRepository<DocumentDetail> receivingDetailRepository)
        {
            _receivingRepository = receivingRepository;
            _receivingDetailRepository = receivingDetailRepository;
        }

        public Document GetDocument(long id)
        {
            var repo = _receivingRepository.GetAll()
                .Where(p => p.Id == id);

            if (repo.Count() == 0)
                return null;
            return repo.First();
        }

        public DocumentDetail GetDocumentDetail(long id)
        {
            var repo = _receivingDetailRepository.GetAll()
                .Where(p => p.Id == id).Include(p=> p.Product);

            if (repo.Count() == 0)
                return null;
            return repo.First();
        }


        public void CreateDocument(Document document)
        {
            _receivingRepository.Insert(document);

        }
        public void CreateDocumentDetail(DocumentDetail documentDetail)
        {
            _receivingDetailRepository.Insert(documentDetail);

        }

        public List<Document> GetDocumentList()
        {
            var documentList = _receivingRepository.GetAll();

            return documentList.Include(prop => prop.Client).ToList();

        }
        //public List<Document> GetDocumentDetailList()
        //{
        //    var documentDetailList = _receivingRepository.GetAll();

        //    return documentDetailList.Include(prop => prop.Client).ToList();

        //}


        public List<DocumentDetail> GetDocumentDetailListByDocumentId(int documentId)
        {
            var documentDetailList = _receivingDetailRepository.GetAll().Where(p => p.DocumentId == documentId).Include(p=>p.Product);

            return documentDetailList.ToList();

        }

        public void DeleteDocument(Document entity)
        {
            _receivingRepository.Delete(entity);
        }

        public void DeleteDocumentDetail(DocumentDetail entity)
        {
            _receivingDetailRepository.Delete(entity);
        }

        public IQueryable<Document> GetDocumentDetailList()
        {
            var documentDetailList = _receivingRepository.GetAll();

            return documentDetailList;

        }

       
    }

}
