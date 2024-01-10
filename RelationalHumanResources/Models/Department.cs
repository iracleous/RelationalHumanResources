namespace RelationalHumanResources.Models
{
    public class Department
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Employee> Employees { get; set; }
        = [];

    }
}
