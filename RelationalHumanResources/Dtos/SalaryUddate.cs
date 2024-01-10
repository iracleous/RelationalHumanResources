namespace RelationalHumanResources.Dtos
{
    public class SalaryUpdate
    {
        public long EmployeeId { get; set; }
        public decimal SalaryAmount { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
