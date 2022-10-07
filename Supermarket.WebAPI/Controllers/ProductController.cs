using Supermarket.Model;
using Supermarket.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;

namespace Supermarket.WebAPI.Controllers
{
    
    
    public class ProductController : ApiController
    {

        public ProductService Service { get; set; }
        public ProductController()
        {
            Service = new ProductService();
        }

        // GET: api/product
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllProducts()
        {
            
            List<Product> products = await Service.GetAllProductsAsync();
            List<ProductRest> productsRest = MapToREST(products);
            if (productsRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            return Request.CreateResponse<List<ProductRest>>(HttpStatusCode.OK, productsRest);
        }

        // GET: api/product/5
        public async Task<HttpResponseMessage> Get(string name)
        {
            //find product with the id
            List<Product> products = await Service.GetProductAsync(name);
            List<ProductRest> productsRest = MapToREST(products);
            if (productsRest[0].Name == "" || productsRest[0].Name == null)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }

            return Request.CreateResponse<ProductRest>(HttpStatusCode.OK, productsRest[0]);
        }

        // POST: api/Product
        public async Task<HttpResponseMessage> Post([FromBody] Product product)
        {
            bool isPosted = await Service.PostProductAsync(product.Name, product.Price, product.Mark);
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product is not valid");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product has been added");
        }

        // PUT: api/Product/5
        public async Task<HttpResponseMessage> Put(string name, [FromBody] Product product)
        {

            bool isEdited = await Service.EditProductAsync(name, product);
            //enter new employee into db
            if (!isEdited)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product info edited");

        }

        // DELETE: api/Product/5
        public async Task<HttpResponseMessage> Delete(string name)
        {
            bool isDeleted = await Service.DeleteProductAsync(name);
            if (!isDeleted)//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product removed");
        }
        private List<ProductRest> MapToREST(List<Product> products)
        {
            List<ProductRest> productsRest = new List<ProductRest>();
            if (products.Count > 0)
            {
                foreach (Product product in products)
                {
                    ProductRest productRest = new ProductRest(product.Id, product.Name, product.Price);
                    productsRest.Add(productRest);
                }
                return productsRest;
            }
            else
            {
                return null;
            }
        }
        private List<Product> MapToDomain(List<ProductRest> productsRest)
        {
            List<Product> products = new List<Product>();
            if (productsRest.Count > 0)
            {
                foreach (ProductRest productRest  in productsRest)
                {

                    products.Add(new Product(productRest.Id, productRest.Name, productRest.Price,  ""));
                }
                return products;
            }
            else
            {
                return null;
            }
        }
    }
}
