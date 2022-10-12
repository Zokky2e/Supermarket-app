using Supermarket.Common;
using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository;
using Supermarket.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service.Common
{
    public interface IEmployeeService
    {

        Task<List<Employee>> GetAllEmployeesAsync(Paging paging, Sorting sorting, Filtering filtering);
        Task<List<Employee>> GetEmployeeByOIBAsync(string OIB);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<bool> PostEmployeeAsync(IEmployee employee);
        Task<bool> EditEmployeeAsync(string OIB, IEmployee employee);
        Task<bool> DeleteEmployeeAsync(string OIB);
    }
}
