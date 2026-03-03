namespace api;

using Microsoft.AspNetCore.Mvc;
using model;

[ApiController]
[Route("api/v1/[controller]/[Action]")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllEmployeeInfo()
    {
        return this.Ok(EmployeeModel.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployeeById(int id)
    {
        var employee = EmployeeModel.GetBy(id);
        if (employee == null)
        {
            return NotFound($"Employee ID {id} not found.");
        }
        return this.Ok(employee);
    }

    // api/Employees?employee_id=id
    [HttpGet("api/Employees")]
    public IActionResult GetEmployeeById2([FromQuery] int id)
    {
        var employee = EmployeeModel.GetBy(id);
        if (employee == null)
        {
            return NotFound($"Employee ID {id} not found.");
        }
        return this.Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] Employee employee)
    {
        return Ok(EmployeeModel.Create(employee));
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
    {
        employee.Id = id;

        return Ok(EmployeeModel.Update(employee));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = EmployeeModel.GetBy(id);
        if (employee == null)
        {
            return NotFound($"Employee ID {id} not found.");
        }
        return Ok(EmployeeModel.DeleteEmployee(id));
    }

}
