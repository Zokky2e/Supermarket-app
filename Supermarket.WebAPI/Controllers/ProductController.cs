using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Supermarket.WebAPI.Controllers
{
    public class Product
    {
        public string Key { get; set; }
        public Product(string key)
        {
            Key = key;
        }
    }
    public class ProductManager
    {
        static public List<Product> Products { get; set; }
        public ProductManager()
        {
            Products = new List<Product>() { new Product("apple") };
        }
    }
    public class ProductController : ApiController
    {

        public ProductManager Manager { get; set; }
        public ProductController()
        {
            Manager = new ProductManager();
        }

        // GET: api/product
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            //get products from db

            if (ProductManager.Products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }

            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, ProductManager.Products);
        }

        // GET: api/product/5
        public HttpResponseMessage Get(int id)
        {
            //find product with the id
            if (ProductManager.Products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= ProductManager.Products.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }

            return Request.CreateResponse<Product>(HttpStatusCode.OK, ProductManager.Products[id]);
        }

        // POST: api/Product
        public HttpResponseMessage Post([FromBody] Product product)
        {

            if (product.Key == null || product.Key == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product is not valid");
            }
            //enter new product into db
            ProductManager.Products.Add(product);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product has been added");
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put(int id, [FromBody] Product product)
        {
            //enter new employee into db
            if (ProductManager.Products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= ProductManager.Products.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }
            else if (product.Key == null || product.Key == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product info is not valid");
            }
            ProductManager.Products[id] = product;
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product info edited");

        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id)
        {
            //find product with the id
            if (ProductManager.Products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= ProductManager.Products.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }
            ProductManager.Products.Remove(ProductManager.Products[id]);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product removed");
        }
    }
}
