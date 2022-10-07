using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Common
{
    public interface IEmployeeRest
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string OIB { get; set; }
        
    }
}
