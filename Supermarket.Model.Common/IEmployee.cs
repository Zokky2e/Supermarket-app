using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Common
{
    public interface IEmployee
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime Birthday { get; set; }
        string Address { get; set; }
        string OIB { get; set; }
    }
}
