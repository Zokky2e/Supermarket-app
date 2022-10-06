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
        List<Employee> GetAllEmployees();
        Employee GetEmployee(string OIB);
        bool PostEmployee(string FirstName, string LastName, string Address, string OIB);
        bool EditEmployee(string OIB, Employee employee);
        bool DeleteEmployee(string OIB);
    }
}
