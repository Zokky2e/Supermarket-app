using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository;
using Supermarket.Repository.Common;
using Supermarket.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await Repository.GetAllEmployeesAsync();
            return employees;
        }
        public async Task<List<Employee>> GetEmployeeAsync(string OIB)
        {
            List<Employee> employees = new List<Employee>();
            employees.AddRange(await Repository.GetEmployeeAsync(OIB));
            return employees;
        }
        public async Task<bool> PostEmployeeAsync(IEmployee employee)
        {
            return await Repository.PostEmployeeAsync(employee);
        }
        public async Task<bool> EditEmployeeAsync(string OIB, IEmployee employee)
        {
            return await Repository.EditEmployeeAsync(OIB, employee);
        }
        public async Task<bool> DeleteEmployeeAsync(string OIB)
        {
            return await Repository.DeleteEmployeeAsync(OIB);
        }
    }
}
