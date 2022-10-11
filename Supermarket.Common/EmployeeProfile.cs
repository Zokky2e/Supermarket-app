using AutoMapper;
using Supermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Common
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeRest>()
                .ConstructUsing(employee => new EmployeeRest());
            CreateMap<EmployeeRest, Employee>()
                    .ConstructUsing(employeeRest => new Employee());
        }
    }
}
