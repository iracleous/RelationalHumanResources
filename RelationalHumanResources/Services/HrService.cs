using Microsoft.EntityFrameworkCore;
using RelationalHumanResources.Data;
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


    public ApiResult<List<Department>> GetAllDepartment();
    public ApiResult<Department> CreateDepartment(Department department);

    public ApiResult<bool> AssignEmployeeToDepartment(long  employeeId, 
        long departmentId);
}

public class HrService : IHrService
{
    private readonly HrDbcontext _context;
    public HrService(HrDbcontext context)
    {
        _context = context;
    }

    public ApiResult<bool> AssignEmployeeToDepartment(long employeeId, long departmentId)
    {
        var employee = _context.Employees.Find(employeeId);
        if (employee == null)
            return new ApiResult<bool>
            {       
                Data = false,
                Status=1, 
                Message="Employee not found" 
             };
        var department = _context.Departments.Find(departmentId);
        if (department == null)
            return new ApiResult<bool>
            {
                Data = false,
                Status = 1,
                Message = "Department not found"
            };
        employee.Department = department;
        try
        {
            _context.SaveChanges();

            return new ApiResult<bool>
            {
                Data = true,
                Status = 0,
                Message = "Assignment has been made"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<bool>
            {
                Data = false,
                Status = 3,
                Message = "Exception: " + ex.Message
            };
        }
    }

    public ApiResult<Department> CreateDepartment(Department department)
    {
        try
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return new ApiResult<Department>
            {
                Data = department,
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<Department>
            {
                Status = 3,
                Message = "Exception: " + ex.Message
            };
        }
    }

    public ApiResult<Employee> CreateEmployee(Employee employee)
    {
        try
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return new ApiResult<Employee>
            {
                Data = employee,
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<Employee>
            {
                Status = 3,
                Message = "Exception: " + ex.Message
            };
        }
    }

    public ApiResult<bool> DeleteEmployee(long employeeId)
    {
        var employee = _context.Employees.Find(employeeId);
        if (employee == null) 
            return new ApiResult<bool>
            {
                Data = false,
                Status = 1,
                Message = "employee not found"
            };
        
        try
        {
            _context.Remove(employee);
            _context.SaveChanges() ;
            return new ApiResult<bool>
            {
                Data = true,
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<bool>
            {
                Data = false,
                Status = 3,
                Message = "Exception: " + ex.Message
            };
        }
    }

    public ApiResult<List<Department>> GetAllDepartment()
    {
        try
        {
            return new ApiResult<List<Department>>
            {
                Data = _context
                   .Departments
                    .ToList(),
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<List<Department>>
            {
                Status = 3,
                Message = ex.Message
            };
        }
    }

    public ApiResult<List<Employee>> GetAllEmployees()
    {
        try
        {
            return new ApiResult<List<Employee>>
            {
                Data = _context
               .Employees
               .Include(employee => employee.Department)
               .ToList(),
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<List<Employee>>
            {
                Status = 3,
                Message = ex.Message
            };
        }
    }

    public ApiResult<Employee> GetEmployee(long employeeId)
    {
        try
        {
            return new ApiResult<Employee>
            {
                Data = _context.Employees
             .Where(employee => employee.Id == employeeId)
           .Include(employee => employee.Department)
            .Single(),
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<Employee>
            {
                Status = 3,
                Message = ex.Message
            };
        }
    }

    public ApiResult<bool> UpdateEmployee(SalaryUpdate salaryUpdate)
    {
         if (salaryUpdate == null) return  new ApiResult<bool>
         {
             Status = 4,
             Message = "null input"
         };  
         var employee = _context
            .Employees
            .Find(salaryUpdate.EmployeeId);

        if (employee == null) return new ApiResult<bool>
        {
            Status = 1,
            Message = "employee not found"
        }; ;

        if (salaryUpdate.SalaryAmount <= Constants.BasicSalary)
        {
            return new ApiResult<bool>
            {
                Status = 5,
                Message = "not acceptable salary"
            }; ;
        }
        var salaryHistory = new SalaryHistory
        {
            Employee = employee,
            Amount = salaryUpdate.SalaryAmount,
            Active = true,
            StartingDate = salaryUpdate.StartDate
        };
        try
        {
            _context.SalariesHistory.Add(salaryHistory);
            _context.SaveChanges();

            return new ApiResult<bool>
            {
                Data = true,
                Status = 0,
                Message = "ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<bool>
            {
                Status = 3,
                Message = ex.Message
            };
        }

    }
}
