using AutoMapper;
using Supermarket.Common;
using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Service;
using Supermarket.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Supermarket.WebAPI.Controllers
{
    public class EmployeeProductController : ApiController
    {
        private IEmployeeProductService Service { get; set; }
        private IEmployeeService EmployeeService { get; set; }
        private IProductService ProductService { get; set; }
        private IMapper Mapper;
        public EmployeeProductController(IMapper mapper, IEmployeeProductService service, IEmployeeService employeeService, IProductService productService)
        {
            Service = service;
            EmployeeService = employeeService;
            ProductService = productService;
            Mapper = mapper;
        }
        // GET: api/employee
        public async Task<HttpResponseMessage> GetAllEmployeesAsync()
        {


            List<EmployeeProduct> employeeProducts = await Service.GetAllEmployeeProductsAsync();
            List<EmployeeProductRest> employeeProductsRest = MapToREST(employeeProducts);
            if (employeeProductsRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            return Request.CreateResponse<List<EmployeeProductRest>>(HttpStatusCode.OK, employeeProductsRest);
        }

        // GET: api/employee/5
        public async Task<HttpResponseMessage> GetAsync(Guid Id)
        {
            List<EmployeeProduct> employeeProducts = await Service.GetEmployeeProductAsync(Id);
            List<EmployeeProductRest> employeeProductsRest = MapToREST(employeeProducts);

            if (employeeProductsRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found!");
            }
            return Request.CreateResponse<List<EmployeeProductRest>>(HttpStatusCode.OK, employeeProductsRest);
        }

        // POST: api/employee
        public async Task<HttpResponseMessage> PostAsync([FromBody] EmployeeProductRest employeeProductRest)
        {
            if (employeeProductRest.Id == Guid.Empty) employeeProductRest.Id = Guid.NewGuid();
            bool isPosted = await Service.PostEmployeeProductAsync(MapToDomain(new List<EmployeeProductRest> { employeeProductRest }).First());
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }


            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee has been added");
        }

        // PUT: api/employee/5
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] EmployeeProductRest employeeProductRest)
        {

            if (employeeProductRest.Id == Guid.Empty) employeeProductRest.Id = Guid.NewGuid();
            bool isEdited = await Service.EditEmployeeProductAsync(id, MapToDomain(new List<EmployeeProductRest> { employeeProductRest }).First());

            if (!isEdited)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Something went wrong!");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee info edited");

        }

        // DELETE: api/employee/5
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            bool isDeleted = await Service.DeleteEmployeeProductAsync(id);
            if (!isDeleted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee removed");
        }

        private List<EmployeeProductRest> MapToREST(List<EmployeeProduct> employeeProducts)
        {
            List<EmployeeProductRest> employeeProductsRest = new List<EmployeeProductRest>();
            if (employeeProducts.Count > 0)
            {
                foreach (EmployeeProduct employeeProduct in employeeProducts)
                {
                    EmployeeProductRest employeeProductRest =
                        new EmployeeProductRest(employeeProduct.Id,
                        EmployeeService.GetEmployeeByIdAsync(employeeProduct.EmployeeId).Result.OIB,
                        ProductService.GetProductByIdAsync(employeeProduct.ProductId).Result.Name);
                    employeeProductsRest.Add(employeeProductRest);
                }
                return employeeProductsRest;
            }
            else
            {
                return employeeProductsRest;
            }
        }
        private List<EmployeeProduct> MapToDomain(List<EmployeeProductRest> employeeProductsRest)
        {
            List<EmployeeProduct> employeeProducts = new List<EmployeeProduct>();
            if (employeeProductsRest.Count > 0)
            {
                foreach (EmployeeProductRest employeeProductRest in employeeProductsRest)
                {
                    Guid employeeId = EmployeeService.GetEmployeeByOIBAsync(employeeProductRest.EmployeeOIB).Result[0].Id;
                    Guid productId = ProductService.GetProductByNameAsync(employeeProductRest.ProductName).Result[0].Id;
                    EmployeeProduct employeeProduct = new EmployeeProduct(employeeProductRest.Id, employeeId,productId);
                    employeeProducts.Add(employeeProduct);
                }
                return employeeProducts;
            }
            else
            {
                return employeeProducts;
            }
        }
    }

}
