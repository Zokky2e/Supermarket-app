using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository;
using Supermarket.Repository.Common;
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
        private IProductRepository Repository { get; set; }

        public ProductService(IProductRepository repository)
        {
            Repository = repository;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = await Repository.GetAllProductsAsync();
            return products;
        }
        public async Task<List<Product>> GetProductAsync(string name)
        {
            List<Product> products = new List<Product>();
            products.AddRange(await Repository.GetProductAsync(name));
            return products;
        }
        public async Task<bool> PostProductAsync(IProduct product)
        {
            return await Repository.PostProductAsync(product);
        }
        public async Task<bool> EditProductAsync(string name, IProduct product)
        {
            return await Repository.EditProductAsync(name, product);
        }
        public async Task<bool> DeleteProductAsync(string name)
        {
            return await Repository.DeleteProductAsync(name);
        }
    }

}
