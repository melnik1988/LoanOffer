namespace LoanOffer.Moduls.Classes
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double[] RequestingLoan { get; set; }
        public int PeriodInMonths { get; set; }
    }
}
