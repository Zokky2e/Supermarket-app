using Supermarket.Common;
using Supermarket.Model;
using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync(Paging paging, Sorting sorting, Filtering filtering);
        Task<List<Employee>> GetEmployeeAsync(string OIB);
        Task<bool> PostEmployeeAsync(IEmployee employee);
        Task<bool> EditEmployeeAsync(string OIB, IEmployee employee);
        Task<bool> DeleteEmployeeAsync(string OIB);
    }
}
