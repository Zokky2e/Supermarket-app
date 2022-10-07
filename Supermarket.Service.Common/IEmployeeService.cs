using Supermarket.Model;
using Supermarket.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service.Common
{
    public interface IEmployeeService
    {
        EmployeeRepository EmployeeRepository { get; set; }
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeeAsync(string OIB);
        Task<bool> PostEmployeeAsync(string FirstName, string LastName, string Address, string OIB);
        Task<bool> EditEmployeeAsync(string OIB, Employee employee);
        Task<bool> DeleteEmployeeAsync(string OIB);
    }
}
