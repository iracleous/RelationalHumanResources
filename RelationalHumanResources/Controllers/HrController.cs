using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationalHumanResources.Data;
using RelationalHumanResources.Dtos;
using RelationalHumanResources.Models;
using RelationalHumanResources.Services;

namespace RelationalHumanResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrController : ControllerBase
    {
        private readonly IHrService _service;

        public HrController(IHrService service) 
        {
            _service = service;
        }

        [HttpGet]
        [Route("employee")]
        public ApiResult<List<Employee>> GetAllEmployees()
        {
          return _service.GetAllEmployees();
        }

        [HttpPost]
        [Route("employee")]
        public ApiResult<Employee> Post(Employee employee)
        {
            return _service.CreateEmployee(employee);
        }

        [HttpPost]
        [Route("department")]
        public async Task<ApiResult<Department>> Post(Department department)
        {
            return await _service.CreateDepartmentAsync(department);
        }

        [HttpGet]
        [Route("department")]
        public async Task<ApiResult<List<Department>>> GetAllDepartments()
        {
            return await _service.GetAllDepartmentAsync();
        }

        [HttpPost]
        [Route("department/employee")]
        public ApiResult<bool> AssignEmployeeToDepartment(
            [FromBody] EmployeeDepartmentDto employeeDepartment)
        {
            return _service.AssignEmployeeToDepartment(employeeDepartment);
        }

        [HttpDelete]
        [Route("department/employee")]
        public async Task<ApiResult<bool>> RemoveEmployeeFromDepartment(
           [FromBody] EmployeeDepartmentDto employeeDepartment)
        {
            return await _service.RemoveEmployeeFromDepartmentAsync(employeeDepartment);
        }

 




        [HttpDelete]
        [Route("employee/{employeeId}")]
        public ApiResult<bool> DeleteEmployee(long employeeId)
        {
            return _service.DeleteEmployee(employeeId);
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public ApiResult<Employee> GetEmployee(long employeeId)
        {
            return _service.GetEmployee(employeeId);
        }

        [HttpPost]  
         [Route("employee/salary")]
         public ApiResult<bool> UpdateEmployeeSalary(SalaryUpdate salaryUpdate)
        {
            return _service.UpdateEmployee(salaryUpdate);
        }
    }
}
