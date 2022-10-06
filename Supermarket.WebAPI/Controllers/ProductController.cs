using Supermarket.Model;
using Supermarket.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage GetAllProducts()
        {
            
            List<Product> products = Service.GetAllProducts();
            if (products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, products);
        }

        // GET: api/product/5
        public HttpResponseMessage Get(string name)
        {
            //find product with the id
            Product product = Service.GetProduct(name);
            if (product.Name == "" || product.Name == null)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }

            return Request.CreateResponse<Product>(HttpStatusCode.OK, product);
        }

        // POST: api/Product
        public HttpResponseMessage Post([FromBody] Product product)
        {
            bool isPosted = Service.PostProduct(product.Name, product.Price, product.Mark);
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product is not valid");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product has been added");
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put(string name, [FromBody] Product product)
        {

            bool isEdited = Service.EditProduct(name, product);
            //enter new employee into db
            if (!isEdited)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product info edited");

        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(string name)
        {
            bool isDeleted = Service.DeleteProduct(name);
            if (!isDeleted)//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product removed");
        }
    }
}
