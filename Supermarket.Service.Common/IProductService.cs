using Supermarket.Model;
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
        ProductRepository ProductRepository { get; set; }
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductAsync(string name);
        Task<bool> PostProductAsync(string name, decimal price, string mark);
        Task<bool> EditProductAsync(string oldName, Product product);
        Task<bool> DeleteProductAsync(string name);
    }
}
