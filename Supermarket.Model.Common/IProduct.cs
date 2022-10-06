using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Common
{
    public interface IProduct
    {
        Guid Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string Mark { get; set; }
    }
}
