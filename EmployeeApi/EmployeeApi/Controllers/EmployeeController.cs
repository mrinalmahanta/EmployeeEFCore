using EmployeeApi.BusinessLogic;
using EmployeeApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [Route("EmployeeService")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IProcessEmployee _processEmployee;

        public EmployeeController(IProcessEmployee processEmployee)
        {
            _processEmployee = processEmployee;
        }

        /// <summary>
        /// Gets a list of Employees
        /// </summary>
        /// <returns>A list of employee objects.</returns>
        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _processEmployee.GetAllEmployees();
            return Ok(result);
        }

        /// <summary>
        /// Gets an employee by id
        /// </summary>
        /// <returns>An employee object.</returns>
        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var result = await _processEmployee.GetEmployee(id);

            return Ok(result);
        }

        /// <summary>
        /// Update an employee
        /// </summary>
        /// <returns>No content.</returns>
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, Employees employees)
        {
            var res = await _processEmployee.UpdateEmployee(id, employees);
            return Ok(res>0?"Updated Successfully":"Not Updated");
        }

        /// <summary>
        /// POST an employee details
        /// </summary>
        /// <remarks>
        /// {
    ///  "employeeId": "e062ed9e1c514b91837a50e74b566437",
    ///  "firstName": "Uncle",
    ///  "lastName": "Bob",
    ///  "emailId": "uncle.bob@gmail.com",
    ///  "age": 0,
    ///  "address": {
    ///    "employeeId": "e062ed9e1c514b91837a50e74b566437",
    ///    "addressId": "49ff251f-b601-4cec-a102-604d54b3b9e7",
    ///    "address1": null,
    ///    "address2": null,
    ///    "city": "Kol",
    ///    "zipcode": 0,
    ///    "state": null,
    ///    "country": null,
    ///    "employees": null
    ///  }
    ///}
    /// </remarks>
    /// <returns>A list of employee objects.</returns>
    [HttpPost("PostEmployee")]
        public async Task<IActionResult> PostEmployee(Employees emp)
        {
            var result = await _processEmployee.AddEmployee(emp);
            return Ok(result > 0 ? "Added Successfully" : "Not Added");
        }

        /// <summary>
        /// Delete an employees
        /// </summary>
        /// <returns>No Content.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteEmp(string id)
        {
            
            var emp = await _processEmployee.GetEmployee(id);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                var result =await _processEmployee.DeleteEmployee(emp);
                return Ok(result > 0 ? "Deleted Successfully" : "Not Deleted");
            }

        }

        /// <summary>
        /// Search an employee details
        /// </summary>
        /// <returns>A list of employee objects.</returns>
        [HttpPost("SearchEmployee")]
        public async Task<IActionResult> SearchEmployee(string firstName, string lastname)
        {
            var result = await _processEmployee.SearchEmployees(firstName,lastname);
            return Ok(result);
        }

    }
}
