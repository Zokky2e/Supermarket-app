using Supermarket.Model;
using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Repository.Common
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductAsync(string name);
        Task<bool> PostProductAsync(IProduct product);
        Task<bool> EditProductAsync(string name, IProduct product);
        Task<bool> DeleteProductAsync(string name);
    }
}
