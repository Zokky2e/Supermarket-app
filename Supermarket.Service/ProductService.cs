using Supermarket.Model;
using Supermarket.Repository;
using Supermarket.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
        public bool PostProduct(string name, decimal price, string mark)
        {
            Product product = new Product(name, price, mark);
            return ProductRepository.PostProduct(product);
        }
        public bool EditProduct(string name, Product product)
        {
            return ProductRepository.EditProduct(name, product);
        }
        public bool DeleteProduct(string name)
        {
            return ProductRepository.DeleteProduct(name);
        }
    }
    
}
