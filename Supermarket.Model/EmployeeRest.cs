using Supermarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class EmployeeRest : IEmployeeRest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OIB { get; set; }
        public EmployeeRest(Guid id, string firstName, string lastName, string oib)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
        }
        public EmployeeRest(string firstName, string lastName, string oib)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
        }
        public EmployeeRest()
        {

        }
    }
}
