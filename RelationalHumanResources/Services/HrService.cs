﻿using Microsoft.EntityFrameworkCore;
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
        _context.Departments.Add(department);
        _context.SaveChanges();
        return department;
    }

    public ApiResult<Employee> CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return employee;
    }

    public ApiResult<bool> DeleteEmployee(long employeeId)
    {
        var employee = _context.Employees.Find(employeeId);
        if (employee == null) return false;
        _context.Remove(employee);
        try
        {
            _context.SaveChanges() ;
        }
        catch { return false; }

        
        return true;
    }

    public ApiResult<List<Department>> GetAllDepartment()
    {
        return _context
               .Departments
                .ToList();
    }

    public ApiResult<List<Employee>> GetAllEmployees()
    {
        return _context
               .Employees
               .Include(employee => employee.Department)
               .ToList();
    }

    public ApiResult<Employee> GetEmployee(long employeeId)
    {
        return _context.Employees
             .Where(employee => employee.Id==employeeId)
           .Include(employee => employee.Department)
            .Single();
       //.Find(employeeId);
    }

    public ApiResult<bool> UpdateEmployee(SalaryUpdate salaryUpdate)
    {
         if (salaryUpdate == null) return false;
         var employee = _context
            .Employees
            .Find(salaryUpdate.EmployeeId);

        if (employee == null) return false;
        
        if (salaryUpdate.SalaryAmount <= Constants.BasicSalary)
        {
            return false;
        }
        var salaryHistory = new SalaryHistory
        {
            Employee = employee,
            Amount = salaryUpdate.SalaryAmount,
            Active = true,
            StartingDate = salaryUpdate.StartDate
        };
        
        _context.SalariesHistory.Add(salaryHistory);
        _context.SaveChanges();

        return true;
    }
}
