using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Products
{
    public class ProductManager : IDomainService, ITransientDependency
    {
        private readonly IRepository<Product, int> _productRepository;
        public ProductManager(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }
        public Product GetProduct(long id)
        {
            var repo = _productRepository.GetAll()
                .Where(p => p.Id == id);

            if (repo.Count() == 0)
                return null;
            return repo.First();
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Insert(product);

        }
        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);

        }
        public void DeleteProduct(Product entity)
        {
            _productRepository.Delete(entity);
        }
        //public List<Product> GetProductList()
        //{
        //    var producttList = _productRepository.GetAll();

        //    return producttList.ToList();

        //}
        public IQueryable<Product> GetProductList()
        {
            var productList = _productRepository.GetAll();

            return productList;

        }



    }

}
