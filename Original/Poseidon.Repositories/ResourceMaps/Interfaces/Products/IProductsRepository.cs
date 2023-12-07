using Poseidon.Entities.ResourceMaps.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Products
{
    public interface IProductsRepository
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(Guid productId);
        public Task<Guid> InsertProductEntity(Product product);
        public Task<bool> UpdateProductEntity(Product product);
        public Task<bool> DeleteProductEntity(Guid productId);
    }
}
