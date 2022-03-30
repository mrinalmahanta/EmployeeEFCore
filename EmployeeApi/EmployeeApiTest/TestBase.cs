using EmployeeApi.BusinessLogic;
using EmployeeApi.Controllers;
using Moq;
using System;
using Xunit;

namespace EmployeeApiTest
{
    public class TestBase
    {
        
        protected EmployeeController Controller { get; }
        protected Mock<IProcessEmployee> MockProcessEmployee;
        protected TestBase()
        {
            MockProcessEmployee = new Mock<IProcessEmployee>();
            Controller = new EmployeeController(MockProcessEmployee.Object);
        }
    }
}
