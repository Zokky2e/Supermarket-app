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
        List<Product> GetAllProducts();
        Product GetProduct(string name);
    }
}
