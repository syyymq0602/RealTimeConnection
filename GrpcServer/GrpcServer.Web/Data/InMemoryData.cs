using GrpcServer.Web.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Web.Data
{
    public class InMemoryData
    {
        public static List<Employee> Employees = new List<Employee>
        {
            new Employee
            {
                Id=1,
                No=1994,
                FirstName="Chandler",
                LastName="Bing",
                Salary=2200
            },
            new Employee
            {
                Id = 2,
                No = 1999,
                FirstName = "Rachel",
                LastName = "Green",
                Salary = 2400
            },
            new Employee
            {
                Id = 3,
                No = 2001,
                FirstName = "LLLL",
                LastName = "CCCC",
                Salary = 2600
            }
        };
    }
}
