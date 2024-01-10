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
        public ApiResult<Department> Post(Department department)
        {
            return _service.CreateDepartment(department);
        }
        [HttpGet]
        [Route("department")]
        public ApiResult<List<Department>> GetAllDepartments()
        {
            return _service.GetAllDepartment();
        }
        [HttpPost]
        [Route("department/{departmentId}/employee/{employeeId}")]
        public ApiResult<bool> AssignEmployeeToDepartment( long departmentId, long employeeId)
        {
            return _service.AssignEmployeeToDepartment(employeeId,departmentId);
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
