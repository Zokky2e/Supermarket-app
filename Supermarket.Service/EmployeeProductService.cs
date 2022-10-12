using Supermarket.Model;
using Supermarket.Model.Common;
using Supermarket.Repository.Common;
using Supermarket.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Service
{
    public class EmployeeProductService : IEmployeeProductService
    {
        private IEmployeeProductRepository Repository { get; set; }
        public EmployeeProductService(IEmployeeProductRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<EmployeeProduct>> GetAllEmployeeProductsAsync()
        {
            List<EmployeeProduct> employeeProducts = await Repository.GetAllEmployeeProductsAsync();
            return employeeProducts;
        }
        public async Task<List<EmployeeProduct>> GetEmployeeProductAsync(Guid Id)
        {
            List<EmployeeProduct> employeeProducts = await Repository.GetEmployeeProductAsync(Id);
            return employeeProducts;
        }
        public async Task<bool> PostEmployeeProductAsync(EmployeeProduct employeeProduct)
        {
            return await Repository.PostEmployeeProductAsync(employeeProduct);
        }
        public async Task<bool> EditEmployeeProductAsync(Guid id, EmployeeProduct employeeProduct)
        {
            return await Repository.EditEmployeeProductAsync(id, employeeProduct);
        }
        public async Task<bool> DeleteEmployeeProductAsync(Guid id)
        {
            return await Repository.DeleteEmployeeProductAsync(id);
        }
    }
}
