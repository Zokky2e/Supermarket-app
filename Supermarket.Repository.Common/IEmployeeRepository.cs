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
        List<Employee> GetAllEmployees();
        Employee GetEmployee(string OIB);
        bool PostEmployee(Employee employee);
        bool EditEmployee(string OIB, Employee employee);
        bool DeleteEmployee(string OIB);
    }
}
