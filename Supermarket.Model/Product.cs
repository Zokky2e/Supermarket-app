using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Mark { get; set; }
        public Product(Guid id, string name, decimal price, string mark)
        {
            Id = id;
            Name = name;
            Price = price;
            Mark = mark;
        }
        public Product()
        {

        }
        public override string ToString()
        {
            return $"Product {Id} - {Name} - {Price} Euros";
        }
    }
}
