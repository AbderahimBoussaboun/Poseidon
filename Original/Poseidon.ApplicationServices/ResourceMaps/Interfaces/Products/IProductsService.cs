using Poseidon.Api.Models.ResourceMaps.Requests.Products;
using Poseidon.Entities.ResourceMaps.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Products
{
    public interface IProductsService
    {
        public Task<List<Product>> GetProducts();
        public Task<Product> GetProductById(Guid id);
        public Task<Guid> InsertProduct(ProductRequest product);
        public Task<bool> DeleteProduct(Guid id);
        public Task<bool> UpdateProduct(Guid productId, ProductRequest product);

    }
}
