using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class Employee  : IEmployee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string OIB { get; set; }
        public Employee(Guid id, string firstName, string lastName, string address, string oib)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            OIB = oib;
        }
        public Employee(string firstName, string lastName, string address, string oib)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            OIB = oib;
        }
        public Employee()
        {

        }
        public override string ToString()
        {
            return $"Employee {Id} - {FirstName} {LastName}";
        }
    }
}
