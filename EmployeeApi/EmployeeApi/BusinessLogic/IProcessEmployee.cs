using EmployeeApi.DataAccess;
using EmployeeApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.BusinessLogic
{
    public interface IProcessEmployee
    {
        Task<int> AddEmployee(Employees employees);
        Task<int> UpdateEmployee(string empId, Employees employees);
        Task<int> DeleteEmployee(Employees employees);
        Task<List<Employees>> GetAllEmployees();
        Task<Employees> GetEmployee(string empId);
        Task<List<Employees>> SearchEmployees(string fName, string lName);
        List<Employees> ProcessEmployeeDeatils(List<Employees> lstEmp, List<EmployeeAddress> address);
        Employees ProcessSingleEmployeeDeatils(string employeeId);

    }   
}
