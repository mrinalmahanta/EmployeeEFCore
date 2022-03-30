using EmployeeApi.DataAccess;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.BusinessLogic
{
    /// <summary>
    /// Proccess Employee
    /// </summary>
    public class ProcessEmployee : IProcessEmployee
    {
        private readonly IApplicationContext _employeContext;
        /// <summary>
        /// Proccess Employee Const
        /// </summary>
        public ProcessEmployee(IApplicationContext employeContext)
        {
            _employeContext = employeContext;
        }
        /// <summary>
        /// Add Employee
        /// </summary>
        public Task<int> AddEmployee(Employees employees)
        {
            _employeContext.Employees.Add(employees); 
            _employeContext.Addresses.Add(employees.Address);
            return _employeContext.SaveToDB();
        }
        /// <summary>
        /// Delete Employee
        /// </summary>
        public Task<int> DeleteEmployee(Employees employees)
        {
            var emplyeeDetails = ProcessSingleEmployeeDeatils(employees.EmployeeId);
            var addressId= emplyeeDetails.Address.AddressId;
            var addressDetails = GetEmployeeAddressDeatils(addressId);
            _employeContext.Employees.Remove(employees);
            _employeContext.Addresses.Remove(addressDetails);
            return _employeContext.SaveToDB();
        }
        /// <summary>
        /// Get All Employees
        /// </summary>
        public async Task<List<Employees>> GetAllEmployees()
        {
            var employees = await _employeContext.Employees.ToListAsync();
            var address= await _employeContext.Addresses.ToListAsync();
            return ProcessEmployeeDeatils(employees,address);
        }
        /// <summary>
        /// Get single Employee
        /// </summary>
        public Task<Employees> GetEmployee(string empId)
        {
            var employeeDetails = Task.Run(() => ProcessSingleEmployeeDeatils(empId));
            return employeeDetails;
        }
      

        /// <summary>
        /// search Employee
        /// </summary>
        public async Task<List<Employees>> SearchEmployees(string fName, string lName)
        {
            var lstEmployees= await SearchEmployeeDeatils(fName, lName);
            return lstEmployees;
        }

      
        /// <summary>
        /// update Employee
        /// </summary>
        public async Task<int> UpdateEmployee(string empId, Employees employees)
        {
            var result = 0;
            try
            {
                var employee = _employeContext.Employees.Find(empId);
                if (employee != null)
                {
                    employee.FirstName = employees.FirstName;
                    employee.LastName = employees.LastName;
                    employee.Age = employees.Age;
                    employee.EmailId = employees.EmailId;
                    var address=_employeContext.Addresses.Find(employees.Address.AddressId);
                    address.Address1 = employees.Address.Address1;
                    address.Address2 = employees.Address.Address2;
                    address.State = employees.Address.State;
                    address.City = employees.Address.City;
                    address.Country = employees.Address.Country;
                    address.Zipcode = employees.Address.Zipcode;
                    _employeContext.Employees.Update(employee);
                    _employeContext.Addresses.Update(address);
                }
                result = await _employeContext.SaveToDB();
            }
            catch(Exception e)
            {

            }
            return result;
        }

        ///<summary>
        /// Process employee details
        ///</summary>
        public List<Employees> ProcessEmployeeDeatils(List<Employees> lstEmp, List<EmployeeAddress> address)
        {
            var res = from a in lstEmp
                      join b in address on a.EmployeeId
                      equals b.Employees.EmployeeId
                      select new Employees
                      {
                          EmployeeId = a.EmployeeId,
                          FirstName = a.FirstName,
                          LastName = a.LastName,
                          EmailId = a.EmailId,
                          Address = new EmployeeAddress
                          {
                              EmployeeId=a.EmployeeId,
                              AddressId=b.AddressId,
                              Address1=b.Address1,
                              Address2 = b.Address2,
                              City = b.City,
                              Country = b.Country,
                              Zipcode = b.Zipcode
                          },
                      };
            return res.ToList<Employees>();
        }

        ///<summary>
        /// Process single employee details
        ///</summary>
        public Employees ProcessSingleEmployeeDeatils(string employeeId)
        {
            // employee = _employeContext.Employees.Find(employeeId);
            //var address = _employeContext.Addresses.Where()
            var res = from a in _employeContext.Employees
                      join b in _employeContext.Addresses on a.EmployeeId
                      equals b.Employees.EmployeeId
                      where a.EmployeeId == employeeId
                      select new Employees
                      {
                          EmployeeId = a.EmployeeId,
                          FirstName = a.FirstName,
                          LastName = a.LastName,
                          EmailId = a.EmailId,
                          Address = new EmployeeAddress
                          {
                              EmployeeId=a.EmployeeId,
                              AddressId=b.AddressId,
                              City = b.City
                          },
                      };
            return res.FirstOrDefault();
        }

        ///<summary>
        /// Process single employeeAddress details
        ///</summary>
        public EmployeeAddress GetEmployeeAddressDeatils(string addressId)
        {
            var address = _employeContext.Addresses.Where(c=>c.AddressId == addressId).FirstOrDefault();    
            return address;
           
        }

        ///<summary>
        /// Search employee details
        ///</summary>
        public async Task<List<Employees>> SearchEmployeeDeatils(string fName, string lName)
        {
            var res = from a in _employeContext.Employees
                      join b in _employeContext.Addresses on a.EmployeeId
                      equals b.Employees.EmployeeId
                      where a.FirstName==fName || a.LastName==lName
                      select new Employees
                      {
                          EmployeeId = a.EmployeeId,
                          FirstName = a.FirstName,
                          LastName = a.LastName,
                          EmailId = a.EmailId,
                          Address = new EmployeeAddress
                          {
                              AddressId = b.AddressId,
                              City = b.City
                          },
                      };
            var result=  res.ToList<Employees>();
            return result;
        }
    }
}
