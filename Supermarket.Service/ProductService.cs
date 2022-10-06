using Supermarket.Model;
using Supermarket.Repository;
using Supermarket.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service
{
    public class ProductService : IProductService
    {
        public ProductRepository ProductRepository { get; set; }

        public ProductService()
        {
            ProductRepository = new ProductRepository();
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = ProductRepository.GetAllProducts();
            return products;
        }
        public Product GetProduct(string name)
        {
            Product product = ProductRepository.GetProduct(name);
            return product;
        }
    }
    
}
