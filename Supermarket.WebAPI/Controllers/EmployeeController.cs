using AutoMapper;
using Supermarket.Common;
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


    public class EmployeeController : ApiController
    {
        private IEmployeeService Service { get; set; }
        private IMapper Mapper;
        public EmployeeController(IMapper mapper, IEmployeeService service)
        {
            Service = service;
            Mapper = mapper;
        }
        // GET: api/employee
        public async Task<HttpResponseMessage> GetAllEmployeesAsync
            (
            string query = "",
            DateTime? bornBefore = null,
            DateTime? bornAfter = null,
            bool hasAddress = false,
            string sortBy = "LastName",
            string sortOrder = "desc",
            int pageSize = 10,
            int pageNumber = 1
            )
        {

            Filtering filtering = new Filtering(
                bornBefore,
                bornAfter,
                query,
                hasAddress);
            Sorting sorting = new Sorting(
                sortBy, sortOrder);
            Paging paging = new Paging(pageSize, pageNumber);
            List<Employee> employees = await Service.GetAllEmployeesAsync(paging, sorting, filtering);
            List<EmployeeRest> employeesRest = MapToREST(employees);
            if (employeesRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }

        // GET: api/employee/5
        public async Task<HttpResponseMessage> GetEmployeeByOIBAsync(string OIB)
        {
            List<Employee> employees = await Service.GetEmployeeByOIBAsync(OIB);
            List<EmployeeRest> employeesRest = MapToREST(employees);

            if (employeesRest.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found!");
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }
        public async Task<HttpResponseMessage> GetEmployeeByIdAsync(Guid id)
        {
            Employee employee = await Service.GetEmployeeByIdAsync(id);
            EmployeeRest employeeRest = Mapper.Map<EmployeeRest>(employee);

            if (employeeRest.OIB=="")
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found!");
            }
            return Request.CreateResponse<EmployeeRest>(HttpStatusCode.OK, employeeRest);
        }
        // POST: api/employee
        public async Task<HttpResponseMessage> Post([FromBody] EmployeeRest employee)
        {
            if (employee.Id == Guid.Empty) employee.Id = Guid.NewGuid();
            bool isPosted = await Service.PostEmployeeAsync(Mapper.Map<Employee>(employee));
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
            bool isEdited = await Service.EditEmployeeAsync(OIB, Mapper.Map<Employee>(employee));

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
            List<EmployeeRest> employeesRest = new List<EmployeeRest>() { };
            if (employees.Count > 0)
            {
                foreach (Employee employee in employees)
                {
                    EmployeeRest employeeRest = Mapper.Map<EmployeeRest>(employee);
                    employeesRest.Add(employeeRest);
                }
                return employeesRest;
            }
            else
            {
                return employeesRest;
            }
        }
        private List<Employee> MapToDomain(List<EmployeeRest> employeesRest)
        {
            List<Employee> employees = new List<Employee>() { };
            if (employeesRest.Count > 0)
            {
                foreach (EmployeeRest employeeRest in employeesRest)
                {
                    Employee employee = Mapper.Map<Employee>(employeeRest);
                    employees.Add(employee);
                }
                return employees;
            }
            else
            {
                return employees;
            }
        }
    }
}
