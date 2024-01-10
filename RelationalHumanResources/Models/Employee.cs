using System.ComponentModel.DataAnnotations;

namespace RelationalHumanResources.Models
{
    public class Employee
    {
        public long Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public DateOnly? HiringDate { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<SalaryHistory>
            SalaryHistory
        { get; set; } = [];
 

    }
}
