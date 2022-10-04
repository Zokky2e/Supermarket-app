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
        public IHttpActionResult Get()
        {
            
            List<string> employees = new List<string>();
            //get employees from db
            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // GET: api/employee/5
        public IHttpActionResult Get(int id)
        {
            //find employee with the id
            if (id == 0)//simulating not found
            {
                return NotFound();
            }
            return Ok(id);
        }

        // POST: api/employee
        public IHttpActionResult Post([FromBody] Employee employee)
        {

            if (employee.Name == null || employee.Name == "")
            {
                return BadRequest("Invalid data.");
            }
            //enter new employee into db
            return Ok();
        }

        // PUT: api/employee/5
        public IHttpActionResult Put(int id, [FromBody] Employee employee)
        {
            //enter new employee into db
            if (id == 0)
            {
                return NotFound();
            }
            else if (employee.Name == null || employee.Name == "")
            {
                return BadRequest("Not a valid model");
            }
            return Ok();
        }

        // DELETE: api/employee/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Employee id");
            return Ok();
        }
    }
}
