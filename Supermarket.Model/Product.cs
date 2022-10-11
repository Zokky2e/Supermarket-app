using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Supermarket.Model
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Mark { get; set; }
        
        public Product()
        {

        }
        public override string ToString()
        {
            return $"Product {Id} - {Name} - {Price} Euros";
        }
    }
}
