namespace RelationalHumanResources.Models
{
    public class SalaryHistory
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly StartingDate { get; set; }
        public DateOnly EndingDate { get; set; }
        public bool Active { get; set; }

        public virtual Employee Employee { get; set; }

    
    }
}
