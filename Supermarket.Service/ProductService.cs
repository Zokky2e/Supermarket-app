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
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = await ProductRepository.GetAllProductsAsync();
            return products;
        }
        public async Task<List<Product>> GetProductAsync(string name)
        {
            List<Product> products = new List<Product> { await ProductRepository.GetProductAsync(name) };
            return products;
        }
        public async Task<bool> PostProductAsync(string name, decimal price, string mark)
        {
            Product product = new Product(name, price, mark);
            return await ProductRepository.PostProductAsync(product);
        }
        public async Task<bool> EditProductAsync(string name, Product product)
        {
            return await ProductRepository.EditProductAsync(name, product);
        }
        public async Task<bool> DeleteProductAsync(string name)
        {
            return await ProductRepository.DeleteProductAsync(name);
        }
    }
    
}
