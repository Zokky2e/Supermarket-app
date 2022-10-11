using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class Employee : IEmployee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string OIB { get; set; }
        
        public Employee()
        {

        }
        
        public override string ToString()
        {
            return $"Employee {Id} - {FirstName} {LastName}";
        }
    }
}
