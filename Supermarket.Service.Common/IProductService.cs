using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service.Common
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductByNameAsync(string name);
        Task<Product> GetProductByIdAsync(Guid id);

        Task<bool> PostProductAsync(IProduct product);
        Task<bool> EditProductAsync(string oldName, IProduct product);
        Task<bool> DeleteProductAsync(string name);
    }
}
