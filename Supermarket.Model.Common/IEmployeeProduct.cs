using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Common
{
    public interface IEmployeeProduct
    {
        Guid Id { get; set; }
        Guid EmployeeId { get; set; }
        Guid ProductId { get; set; }
    }
}
