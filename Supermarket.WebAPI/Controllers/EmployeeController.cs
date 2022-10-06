﻿using Supermarket.Model;
using Supermarket.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace Supermarket.WebAPI.Controllers
{
    
    
    public class EmployeeController : ApiController
    {
        public EmployeeService Service { get; set; }
        public EmployeeController()
        {
            Service = new EmployeeService();
        }
        // GET: api/employee
        public HttpResponseMessage GetAllEmployees()
        {
            List<Employee> employees = Service.GetAllEmployees();
            if (employees.Count == 0)
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "List of employees is empty");
            }
            return Request.CreateResponse<List<Employee>>(HttpStatusCode.OK, employees);
        }

        // GET: api/employee/5
        public HttpResponseMessage Get(string OIB)
        {
            Employee employee = Service.GetEmployee(OIB);
            //find employee with the oib
            if (employee.OIB == null || employee.OIB == "")
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found!");
            }
            return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
        }

        // POST: api/employee
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            bool isPosted = Service.PostEmployee(employee.FirstName, employee.LastName, employee.Address, employee.OIB);
            if (!isPosted)
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Employee info is not valid");
            }
            //enter new employee into db
            
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee has been added");
        }

        // PUT: api/employee/5
        public HttpResponseMessage Put(string OIB, [FromBody] Employee employee)
        {
            Employee oldEmployee = Service.GetEmployee(OIB);
            //enter new employee into db
            if (oldEmployee.OIB == null || oldEmployee.OIB=="")
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee info edited");

        }

        // DELETE: api/employee/5
        public HttpResponseMessage Delete(string OIB)
        {
            //find product with the oib
            Employee employee = Service.GetEmployee(OIB);
            if (employee.OIB == null || employee.OIB == "" )
            {
                return Request.CreateResponse<string>(HttpStatusCode.NotFound, "Employee not found");
            }
            //simulate delete
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Employee removed");
        }
    }
}
