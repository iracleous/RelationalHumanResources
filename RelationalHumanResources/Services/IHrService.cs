using RelationalHumanResources.Dtos;
using RelationalHumanResources.Models;

namespace RelationalHumanResources.Services;

public interface IHrService
{
    public ApiResult<List<Employee>> GetAllEmployees();
    public ApiResult<Employee> CreateEmployee(Employee employee);
    public ApiResult<bool> UpdateEmployee(SalaryUpdate salaryUpdate);
    public ApiResult<bool> DeleteEmployee(long employeeId);
    public ApiResult<Employee> GetEmployee(long employeeId);

    public Task<ApiResult<List<Department>>> GetAllDepartmentAsync(); 
    public Task<ApiResult<Department>> CreateDepartmentAsync(Department department);

    public ApiResult<bool> AssignEmployeeToDepartment(EmployeeDepartmentDto employeeDepartment);
    public Task<ApiResult<bool>> RemoveEmployeeFromDepartmentAsync(
        EmployeeDepartmentDto employeeDepartment);
} 
