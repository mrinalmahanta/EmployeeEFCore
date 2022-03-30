using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApi.DataAccess
{
    public class EmployeeContextSeed
    {
        public static void SeedAsync(EmployeeContext employeeContext)
        {
            if (!employeeContext.Employees.Any())
            {
                var lstEmployee = new List<Employees> 
                {
                    new Employees
                    {
                        EmployeeId=generateID(),
                        FirstName = "Uncle",
                        LastName = "Bob",
                        EmailId = "uncle.bob@gmail.com",
                        Age = 33,
                        Address = new EmployeeAddress { AddressId=Guid.NewGuid().ToString(),City ="Kol"}
                    },
                    new Employees
                    {
                        EmployeeId=generateID(),
                        FirstName = "Jan",
                        LastName = "Kirsten",
                        EmailId = "jan.kirsten@gmail.com",
                        Age = 44,
                        Address = new EmployeeAddress { AddressId=Guid.NewGuid().ToString(), City ="Mum"}
                    }
                };

                var lstAddress = new List<EmployeeAddress>();
                
                employeeContext.Employees.AddRange(lstEmployee);
                foreach(var employee in lstEmployee)
                {
                    lstAddress.Add(employee.Address);
                }
                employeeContext.Addresses.AddRange(lstAddress);
                employeeContext.SaveChanges();
            }
        }

        public static string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
