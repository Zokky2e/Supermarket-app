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
        public Employee(string name)
        {
            Name = name;
        }
    }
    public class EmployeeManager
    {
        static public List<Employee> Employees { get; set; }
        public EmployeeManager()
        {
            Employees = new List<Employee>() { new Employee("Pero") };
        }
    }
    public class EmployeeController : ApiController
    {
        public EmployeeManager Manager { get; set; }
        public EmployeeController()
        {
            Manager = new EmployeeManager();
        }
        // GET: api/employee
        public HttpResponseMessage GetAllEmployees()
        {

            if (EmployeeManager.Employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            //get employees from db
            return Request.CreateResponse<List<Employee>>(HttpStatusCode.OK, EmployeeManager.Employees);
        }

        // GET: api/employee/5
        public HttpResponseMessage Get(int id)
        {
            //find product with the id
            if (EmployeeManager.Employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of products is empty");
            }
            else if (id < 0 || id >= EmployeeManager.Employees.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Product not found");
            }

            return Request.CreateResponse<Employee>(HttpStatusCode.OK, EmployeeManager.Employees[id]);
        }

        // POST: api/employee
        public HttpResponseMessage Post([FromBody] Employee employee)
        {

            if (employee.Name == null || employee.Name == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }
            //enter new product into db
            EmployeeManager.Employees.Add(employee);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee has been added");
        }

        // PUT: api/employee/5
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            //enter new employee into db
            if (EmployeeManager.Employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            else if (id < 0 || id >= EmployeeManager.Employees.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }
            else if (employee.Name == null || employee.Name == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }
            EmployeeManager.Employees[id] = employee;
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee info edited");

        }

        // DELETE: api/employee/5
        public HttpResponseMessage Delete(int id)
        {
            //find product with the id
            if (EmployeeManager.Employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            else if (id < 0 || id >= EmployeeManager.Employees.Count())//simulating not found
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }
            //simulate delete
            EmployeeManager.Employees.Remove(EmployeeManager.Employees[id]);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee removed");
        }
    }
}
