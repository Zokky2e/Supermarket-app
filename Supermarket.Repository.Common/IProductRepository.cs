using Supermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Repository.Common
{
    public interface IProductRepository
    {
       Task< List<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(string name);
        Task<bool> PostProductAsync(Product product);
        Task<bool> EditProductAsync(string name, Product product);
        Task<bool> DeleteProductAsync(string name);
    }
}
