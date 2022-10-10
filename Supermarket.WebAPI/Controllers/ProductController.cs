using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Service;
using Supermarket.Service.Common;
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

        private IProductService Service { get; set; }
        public ProductController(IProductService service)
        {
            Service = service;
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
            List<Product> products = await Service.GetProductAsync(name);
            List<ProductRest> productsRest = MapToREST(products);
            if (productsRest[0].Name==null || productsRest[0].Name == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }

            return Request.CreateResponse<List<ProductRest>>(HttpStatusCode.OK, productsRest);
        }

        // POST: api/Product
        public async Task<HttpResponseMessage> Post([FromBody] ProductRest product)
        {
            if (product.Id == Guid.Empty) product.Id = Guid.NewGuid();
            bool isPosted = await Service.PostProductAsync(new Product(product));
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product is not valid");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product has been added");
        }

        // PUT: api/Product/5
        public async Task<HttpResponseMessage> Put(string name, [FromBody]  ProductRest product)
        {
            if (product.Id == Guid.Empty) product.Id = Guid.NewGuid();
            bool isEdited = await Service.EditProductAsync(name, new Product(product));
            if (!isEdited)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product not found!");
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
                foreach (ProductRest productRest in productsRest)
                {

                    products.Add(new Product(productRest.Id, productRest.Name, productRest.Price, ""));
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
