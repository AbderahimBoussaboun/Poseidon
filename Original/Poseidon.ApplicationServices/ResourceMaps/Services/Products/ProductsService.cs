using Poseidon.Api.Models.ResourceMaps.Requests.Products;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Products;
using Poseidon.Entities.ResourceMaps.Products;
using Poseidon.Repositories.ResourceMaps.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Products
{
    public class ProductsService : IProductsService
    {

        private readonly IProductsRepository _productRepository;

        public ProductsService(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var result= await _productRepository.DeleteProductEntity(id);
            return result;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var result = await _productRepository.GetProductById(id);
            return result;
        }

        public async Task<List<Product>> GetProducts()
        {
            var result= await _productRepository.GetAllProducts();
            return result;
        }

        public async Task<Guid> InsertProduct(ProductRequest product)
        {
            var temp = new Product() { Name = product.Name, Active = product.Active };
            var result= await _productRepository.InsertProductEntity(temp);
            return result;
        }

        public async Task<bool> UpdateProduct(Guid productId,ProductRequest product)
        {
            var resultProduct = await _productRepository.GetProductById(productId);
            if (resultProduct == null) return false;

            var temp = new Product() { Id = product.Id, Name = product.Name, Active = product.Active };
            var result= await _productRepository.UpdateProductEntity(temp);
            return result;
        }
    }
}
