namespace RelationalHumanResources.Models
{
    public class Department
    {
        public long Id { get; set; }
        public DepartmentCategory DepartmentCategory { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Employee> Employees { get; set; }
        = [];

    }
}
