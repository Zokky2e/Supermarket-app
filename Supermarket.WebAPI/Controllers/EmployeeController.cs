using Supermarket.Model;
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


    public class EmployeeController : ApiController
    {
        private IEmployeeService Service { get; set; }
        public EmployeeController(IEmployeeService service)
        {
            Service = service;
        }
        // GET: api/employee
        public async Task<HttpResponseMessage> GetAllEmployeesAsync()
        {
            List<Employee> employees = await Service.GetAllEmployeesAsync();
            List<EmployeeRest> employeesRest = MapToREST(employees);
            if (employeesRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }

        // GET: api/employee/5
        public async Task<HttpResponseMessage> Get(string OIB)
        {
            List<Employee> employees = await Service.GetEmployeeAsync(OIB);
            List<EmployeeRest> employeesRest = MapToREST(employees);

            if (employeesRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found!");
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }

        // POST: api/employee
        public async Task<HttpResponseMessage> Post([FromBody] EmployeeRest employee)
        {
            if (employee.Id == Guid.Empty) employee.Id = Guid.NewGuid();
            bool isPosted = await Service.PostEmployeeAsync(new Employee(employee));
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }


            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee has been added");
        }

        // PUT: api/employee/5
        public async Task<HttpResponseMessage> Put(string OIB, [FromBody] EmployeeRest employee)
        {

            if (employee.Id == Guid.Empty) employee.Id = Guid.NewGuid();
            bool isEdited = await Service.EditEmployeeAsync(OIB, new Employee(employee));

            if (!isEdited)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Something went wrong!");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee info edited");

        }

        // DELETE: api/employee/5
        public async Task<HttpResponseMessage> Delete(string OIB)
        {
            bool isDeleted = await Service.DeleteEmployeeAsync(OIB);
            if (!isDeleted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee removed");
        }

        private List<EmployeeRest> MapToREST(List<Employee> employees)
        {
            List<EmployeeRest> employeesRest = new List<EmployeeRest>();
            if (employees.Count > 0)
            {
                foreach (Employee employee in employees)
                {
                    EmployeeRest employeeRest = new EmployeeRest(employee.Id, employee.FirstName, employee.LastName, employee.OIB);
                    employeesRest.Add(employeeRest);
                }
                return employeesRest;
            }
            else
            {
                return null;
            }
        }
        private List<Employee> MapToDomain(List<EmployeeRest> employeesRest)
        {
            List<Employee> employees = new List<Employee>();
            if (employeesRest.Count > 0)
            {
                foreach (EmployeeRest employeeRest in employeesRest)
                {

                    employees.Add(new Employee(employeeRest.Id, employeeRest.FirstName, employeeRest.LastName, employeeRest.OIB, ""));
                }
                return employees;
            }
            else
            {
                return null;
            }
        }
    }
}
