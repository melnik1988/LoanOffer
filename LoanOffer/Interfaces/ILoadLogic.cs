namespace LoanOffer.Interfaces
{
    public interface ILoadLogic
    {
        double LoadRules(int age, double[] requestingLoan, int periodInMonths);
    }
}
