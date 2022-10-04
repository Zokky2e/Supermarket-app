using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Supermarket.WebAPI.Controllers
{
    public class Employee
    {
        public string Name { get; set; }
    }
    public class EmployeeController : ApiController
    {
        // GET: api/employee
        List<string> employees = new List<string>() { "pero", "marko" };
        public HttpResponseMessage GetAllEmployees()
        {

            if (employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            //get employees from db
            return Request.CreateResponse<List<string>>(HttpStatusCode.OK, employees);
        }

        // GET: api/employee/5
        public HttpResponseMessage Get(int id)
        {
            //find product with the id
            if (employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= employees.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, employees[id]);
        }

        // POST: api/employee
        public HttpResponseMessage Post([FromBody] Employee employee)
        {

            if (employee.Name == null || employee.Name == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }
            //enter new product into db
            employees.Add(employee.Name);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee has been added");
        }

        // PUT: api/employee/5
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            //enter new employee into db
            if (employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            else if (id < 0 || id >= employees.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }
            else if (employee.Name == null || employee.Name == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee info edited");

        }

        // DELETE: api/employee/5
        public HttpResponseMessage Delete(int id)
        {
            //find product with the id
            if (employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            else if (id < 0 || id >= employees.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee removed");
        }
    }
}
