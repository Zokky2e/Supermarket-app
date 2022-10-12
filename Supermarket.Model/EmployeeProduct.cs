using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class EmployeeProduct
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ProductId { get; set; }

        public EmployeeProduct(Guid id, Guid employeeId, Guid productId)
        {
            Id = id;
            EmployeeId = employeeId;
            ProductId = productId;
        }                   
                               
    }
}
