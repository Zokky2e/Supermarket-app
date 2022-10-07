using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Common
{
    public  interface IProductRest
    {
        Guid Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}
