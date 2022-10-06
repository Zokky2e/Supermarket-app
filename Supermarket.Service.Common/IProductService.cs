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
        List<Product> GetAllProducts();
        Product GetProduct(string name);
    }
}
