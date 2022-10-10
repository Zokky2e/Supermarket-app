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
        Task<List<Product>> GetProductAsync(string name);
        Task<bool> PostProductAsync(IProduct productRest);
        Task<bool> EditProductAsync(string oldName, IProduct productRest);
        Task<bool> DeleteProductAsync(string name);
    }
}
