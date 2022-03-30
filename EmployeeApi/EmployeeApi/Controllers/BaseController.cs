

using System.Net;
using EmployeeApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Controller]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult GetActionResult(ResponseObject response, bool isFsm = false)
        {
            if (response.StatusCode == HttpStatusCode.OK && response.Result == null)
                return NoContent();

            if (response.StatusCode != HttpStatusCode.OK && response.Result != null && isFsm)
                return StatusCode((int)response.StatusCode, response.Result);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Result);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return StatusCode((int)response.StatusCode, response.Message);
            }
        }
    }
}
