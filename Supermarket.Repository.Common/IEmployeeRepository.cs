using Supermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(string OIB);
        Task<bool> PostEmployeeAsync(EmployeeRest employeeRest, string address = "");
        Task<bool> EditEmployeeAsync(string OIB, Employee employee);
        Task<bool> DeleteEmployeeAsync(string OIB);
    }
}
