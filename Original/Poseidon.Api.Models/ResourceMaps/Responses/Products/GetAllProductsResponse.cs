using Poseidon.Entities.ResourceMaps.Products;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Responses.Products
{
    public class GetAllProductsResponse
    {
        public List<Product> Products { get; set; }
    }
}
