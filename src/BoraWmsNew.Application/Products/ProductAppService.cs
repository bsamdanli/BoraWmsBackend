using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using System.Linq;
using Abp.Linq.Extensions;
using BoraWmsNew.Authorization;
using BoraWmsNew.Products.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Extensions;

namespace BoraWmsNew.Products
{
    [AbpAuthorize(PermissionNames.Pages_Product)]
    public class ProductsAppService : BoraWmsNewAppServiceBase
    {
        private ProductManager _productManager;

        private readonly IRepository<Product, int> _repository;
        public ProductsAppService(
            ProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<int> Restock(int productId)
        {
            var product = await _repository.GetAsync(productId);
            product.Quantity = product.Quantity + 1;
            CurrentUnitOfWork.SaveChanges();
            return product.Quantity;
        }
        public ProductDto GetProduct(long id)
        {
            Product product = _productManager.GetProduct(id);
            return ObjectMapper.Map<ProductDto>(product);
        }
        public List<ProductDto> GetProductAll()
        {
            List<Product> product = _productManager.GetProductList().ToList();
            return ObjectMapper.Map<List<ProductDto>>(product);
        }
        public ProductDto CreateProduct(CreateProductDto productDto)
        {
            if (_productManager.GetProductList().Where(o => o.StockCode == productDto.StockCode).Count() > 0)
                throw new UserFriendlyException(productDto.StockCode + " Stok Kodlu Ürün Zaten Mevcut.");

            Product product = new()
            {
                Name = productDto.Name,
                Quantity = productDto.Quantity,
                Description = productDto.Description,
                StockCode = productDto.StockCode

            };
            _productManager.CreateProduct(product);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ProductDto>(product);
            //return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.AsDto());
        }

        public PagedResultDto<ProductDto> GetProductListPaged(PagedProductResultRequestDto input)
        {
            var repo = _productManager.GetProductList().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Name.Contains(input.Keyword));
            var count = repo.Count();
            repo = repo.OrderBy(p => p.Name).PageBy(input);
            return new PagedResultDto<ProductDto>()
            {
                Items = ObjectMapper.Map<List<ProductDto>>(repo),
                TotalCount = count
            };
        }

        public ProductDto UpdateProduct(ProductDto productDto)
        {

            //if (_productManager.GetProductList().Where(o => o.StockCode == productDto.StockCode).Count() > 0)
            //    throw new UserFriendlyException(productDto.StockCode + " Stok Kodlu Ürün Zaten Mevcut.");

            var product = _productManager.GetProduct(productDto.Id);

            product.Name = String.IsNullOrEmpty(productDto.Name) ? productDto.Name : productDto.Name;
            product.Quantity = (productDto.Quantity != null) ? productDto.Quantity : productDto.Quantity;
            product.Description = String.IsNullOrEmpty(productDto.Description) ? productDto.Description : productDto.Description;
            product.StockCode = String.IsNullOrEmpty(productDto.StockCode) ? productDto.StockCode : productDto.StockCode;


            _productManager.UpdateProduct(product);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ProductDto>(product);
        }
        public void DeleteProduct(int productId)
        {
            var product = _productManager.GetProduct(productId);
            if (product != null)
            {
                _productManager.DeleteProduct(product);
            }
            else
            {
                throw new UserFriendlyException("Olmadı.");
            }
        }
    }

}
