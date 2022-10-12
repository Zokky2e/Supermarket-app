using Supermarket.Model.Common;
using Supermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service.Common
{
    public interface IEmployeeProductService
    {
        Task<List<EmployeeProduct>> GetAllEmployeeProductsAsync();
        Task<List<EmployeeProduct>> GetEmployeeProductAsync(Guid Id);
        Task<bool> PostEmployeeProductAsync(EmployeeProduct employeeProduct);
        Task<bool> EditEmployeeProductAsync(Guid id, EmployeeProduct employeeProduct);
        Task<bool> DeleteEmployeeProductAsync(Guid id);
    }
}
