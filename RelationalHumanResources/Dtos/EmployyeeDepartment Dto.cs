namespace RelationalHumanResources.Dtos
{
    public class EmployeeDepartmentDto
    {
        public long EmployeeId { set; get; }
        public long DepartmentId { set; get; }
        public string? Comment { set; get; }
    }
}
