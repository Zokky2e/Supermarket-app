using Supermarket.Common;
using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository;
using Supermarket.Repository.Common;
using Supermarket.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository Repository { get; set; }

        public EmployeeService(IEmployeeRepository repository)
        {
            Repository = repository;
        }
        public async Task<List<Employee>> GetAllEmployeesAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            List<Employee> employees = await Repository.GetAllEmployeesAsync(paging, sorting, filtering);
            return employees;
        }
        public async Task<List<Employee>> GetEmployeeByOIBAsync(string OIB)
        {
            List<Employee> employees = new List<Employee>();
            employees.AddRange(await Repository.GetEmployeeByOIBAsync(OIB));
            return employees;
        }
        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            Employee employees =(await Repository.GetEmployeeByIdAsync(id));
            return employees;
        }
        public async Task<bool> PostEmployeeAsync(IEmployee employee)
        {
            if (employee.Address == null || employee.Address == "") { employee.Address = ""; }
            return await Repository.PostEmployeeAsync(employee);
        }
        public async Task<bool> EditEmployeeAsync(string OIB, IEmployee employee)
        {
            if (employee.Address == null || employee.Address == "") { employee.Address = ""; }
            return await Repository.EditEmployeeAsync(OIB, employee);
        }
        public async Task<bool> DeleteEmployeeAsync(string OIB)
        {
            return await Repository.DeleteEmployeeAsync(OIB);
        }
    }
}
