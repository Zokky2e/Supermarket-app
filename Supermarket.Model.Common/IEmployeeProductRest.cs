using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Common
{
    public interface IEmployeeProductRest
    {
        Guid Id { get; set; }
        string EmployeeOIB { get; set; }
        string ProductName { get; set; }
    }
}
