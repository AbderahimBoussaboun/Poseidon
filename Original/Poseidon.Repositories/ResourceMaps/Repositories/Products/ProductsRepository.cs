using Poseidon.Entities.ResourceMaps;
using Poseidon.Entities.ResourceMaps.Products;
using Poseidon.Repositories.ResourceMaps.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Products
{

    public class ProductsRepository:GenericRepository, IProductsRepository
    {
        public ProductsRepository(DataContext context) : base(context) { }

        Expression<Func<Product, object>>[] includes = new Expression<Func<Product, object>>[]
           {
                e => e.Applications
           };

        public async Task<List<Product>> GetAllProducts()
        {
            var result = await base.GetAll(includes);
            return result;
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var result = await base.GetById(productId, includes);
            return result;
        }

        public async Task<Guid> InsertProductEntity(Product product)
        {
            var result = await base.InsertEntity(product);
            return result;
        }

        public async Task<bool> UpdateProductEntity(Product product)
        {
            var result = await base.UpdateEntity(product);
            return result;
        }

        public async Task<bool> DeleteProductEntity(Guid productId)
        {
            var result = await base.DeleteEntity<Product>(productId);
            return result;
        }
    }
}
