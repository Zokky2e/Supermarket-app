using Supermarket.Model;
using Supermarket.Repository;
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
        public EmployeeRepository EmployeeRepository { get; set; }

        public EmployeeService()
        {
            EmployeeRepository = new EmployeeRepository();
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await EmployeeRepository.GetAllEmployeesAsync();
            return employees;
        }
        public async Task<List<Employee>> GetEmployeeAsync(string OIB)
        {
            List<Employee> employees = new List<Employee> { await EmployeeRepository.GetEmployeeAsync(OIB) };
            return employees;
        }
        public async Task<bool> PostEmployeeAsync(string firstName, string lastName, string OIB, string address = "")
        {
            EmployeeRest employeeRest = new EmployeeRest(firstName, lastName, OIB);
            if (address == "")
            {
                return await EmployeeRepository.PostEmployeeAsync(employeeRest);
            }
            return await EmployeeRepository.PostEmployeeAsync(employeeRest, address);
        }
        public async Task<bool> EditEmployeeAsync(string OIB, Employee employee)
        {
            return await EmployeeRepository.EditEmployeeAsync(OIB, employee);
        }
        public async Task<bool> DeleteEmployeeAsync(string OIB)
        {
            return await EmployeeRepository.DeleteEmployeeAsync(OIB);
        }
    }
}
