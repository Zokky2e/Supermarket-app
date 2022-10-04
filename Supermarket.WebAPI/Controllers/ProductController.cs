using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Supermarket.WebAPI.Controllers
{
    public class MyData
    {
        public string Key { get; set; }
    }
    public class ProductController : ApiController
    {
        // GET: api/Product
        public IHttpActionResult Get()
        {
            //get products from db
            List<string> products = new List<string>();
            if (products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            //find product with the id
            if (id == 0)//simulating not found
            {
                return NotFound();
            }
            return Ok(id);
        }

        // POST: api/Product
        public IHttpActionResult Post([FromBody] MyData data)
        {
            
            if (data.Key == null || data.Key == "")
            {
                return BadRequest("Invalid data.");
            }
            //enter new product into db
            return Ok();
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(int id, [FromBody]MyData data)
        {
            //enter new product into db
            if (id == 0)
            {
                return NotFound();
            }else if(data.Key == null || data.Key == "")
            {
                return BadRequest("Not a valid model");
            }
            return Ok();
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid product id");
            return Ok();
        }
    }
}
