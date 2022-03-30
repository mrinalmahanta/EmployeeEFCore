using EmployeeApi.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeApiTest
{
    public class CreateTest : TestBase
    {
        [Fact]
        public async Task GetAllEmployeesResult()
        {
           // Act
            var response = await Controller.GetEmployees();

            // Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetEmployeeByIdResult()
        {
            // Arrange
            MockProcessEmployee.Setup(o => o.GetEmployee("1111"))
                .ReturnsAsync(GetEmployees().FirstOrDefault());

            // Act
            var response = await Controller.GetEmployeeById("1111");

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var result = response as OkObjectResult;
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        
        private List<Employees> GetEmployees()
        {
           return
                 new List<Employees>
                {
                    new Employees
                    {
                        EmployeeId="1111",
                        FirstName = "Uncle",
                        LastName = "Bob",
                        EmailId = "uncle.bob@gmail.com",
                        Age = 33,
                        Address = new EmployeeAddress { AddressId="33",City ="Kol"}
                    },
                    new Employees
                    {
                        EmployeeId="2222",
                        FirstName = "Jan",
                        LastName = "Kirsten",
                        EmailId = "jan.kirsten@gmail.com",
                        Age = 44,
                        Address = new EmployeeAddress { AddressId="44", City ="Mum"}
                    }
                };
        }
    }
}
