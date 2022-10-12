using AutoMapper;
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
        private IMapper Mapper { get; set; }
        public ProductController(IMapper mapper, IProductService service)
        {
            Service = service;
            Mapper = mapper;
            
        }

        // GET: api/product
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllProductsAsync()
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
        public async Task<HttpResponseMessage> GetByNameAsync(string name)
        {
            List<Product> products = await Service.GetProductByNameAsync(name);
            List<ProductRest> productsRest = MapToREST(products);
            if (productsRest.Count==0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }

            return Request.CreateResponse<List<ProductRest>>(HttpStatusCode.OK, productsRest);
        }
        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            Product product = await Service.GetProductByIdAsync(id);
            ProductRest productRest = Mapper.Map<ProductRest>(product);
            if (productRest.Price == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found!");
            }

            return Request.CreateResponse<ProductRest>(HttpStatusCode.OK, productRest);
        }

        // POST: api/Product
        public async Task<HttpResponseMessage> PostAsync([FromBody] ProductRest product)
        {
            if (product.Id == Guid.Empty) product.Id = Guid.NewGuid();
            bool isPosted = await Service.PostProductAsync(Mapper.Map(product, new Product()));
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product is not valid");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product has been added");
        }

        // PUT: api/Product/5
        public async Task<HttpResponseMessage> PutAsync(string name, [FromBody]  ProductRest product)
        {
            if (product.Id == Guid.Empty) product.Id = Guid.NewGuid();
            bool isEdited = await Service.EditProductAsync(name, Mapper.Map<Product>(product));
            if (!isEdited)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Product not found!");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Product info edited");

        }

        // DELETE: api/Product/5
        public async Task<HttpResponseMessage> DeleteAsync(string name)
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
            List<ProductRest> productsRest = new List<ProductRest>() { };
            if (products.Count > 0)
            {
                foreach (Product product in products)
                {
                    ProductRest productRest = Mapper.Map<ProductRest>(product);
                    productsRest.Add(productRest);
                }
                return productsRest;
            }
            else
            {
                return productsRest;
            }
        }
        private List<Product> MapToDomain(List<ProductRest> productsRest)
        {
            List<Product> products = new List<Product>() { };
            if (productsRest.Count > 0)
            {
                foreach (ProductRest productRest in productsRest)
                {
                    Product product = Mapper.Map<Product>(productRest);
                    products.Add(product);
                }
                return products;
            }
            else
            {
                return products;
            }
        }
    }
}
