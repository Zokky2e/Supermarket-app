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
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = EmployeeRepository.GetAllEmployees();
            return employees;
        }
        public Employee GetEmployee(string OIB)
        {
            Employee employee = EmployeeRepository.GetEmployee(OIB);
            return employee;
        }
        public bool PostEmployee(string FirstName, string LastName, string Address, string OIB)
        {
            Employee employee = new Employee(FirstName, LastName, Address, OIB);
            return EmployeeRepository.PostEmployee(employee);
        }
    }
}
