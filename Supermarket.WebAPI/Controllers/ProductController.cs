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
    }
    public class ProductController : ApiController
    {
        List<string> products = new List<string>() { "apple" };


        // GET: api/product
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            //get products from db

            if (products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }

            return Request.CreateResponse<List<string>>(HttpStatusCode.OK, products);
        }

        // GET: api/product/5
        public HttpResponseMessage Get(int id)
        {
            //find product with the id
            if (products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= products.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, products[id]);
        }

        // POST: api/Product
        public HttpResponseMessage Post([FromBody] Product product)
        {

            if (product.Key == null || product.Key == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product is not valid");
            }
            //enter new product into db
            products.Add(product.Key);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product has been added");
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put(int id, [FromBody] Product product)
        {
            //enter new employee into db
            if (products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= products.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }
            else if (product.Key == null || product.Key == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product info is not valid");
            }
            products[id] = product.Key;
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product info edited");

        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id)
        {
            //find product with the id
            if (products.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= products.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product removed");
        }
    }
}
