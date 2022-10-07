using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class ProductRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductRest()
        {

        }
        public ProductRest(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public ProductRest(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

    }
}
