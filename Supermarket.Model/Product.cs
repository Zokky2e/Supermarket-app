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
        public Product(Guid id, string name, decimal price, string mark)
        {
            Id = id;
            Name = name;
            Price = price;
            Mark = mark;
        }
        public Product(string name, decimal price, string mark)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Mark = mark;
        }
        public Product()
        {

        }
        public Product(IProductRest product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Mark = "";
        }
        public override string ToString()
        {
            return $"Product {Id} - {Name} - {Price} Euros";
        }
    }
}
