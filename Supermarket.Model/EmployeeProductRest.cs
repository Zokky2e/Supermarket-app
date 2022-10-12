using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class EmployeeProductRest
    {
        public Guid Id { get; set; }
        public string EmployeeOIB { get; set; }
        public string ProductName { get; set; }
        public EmployeeProductRest(Guid id, string employeeOIB, string productName)
        {
            Id = id;
            EmployeeOIB = employeeOIB;
            ProductName = productName;
        }
        public EmployeeProductRest(string employeeOIB, string productName)
        {
            EmployeeOIB = employeeOIB;
            ProductName = productName;
        }
        public EmployeeProductRest()
        {

        }
    }
}
