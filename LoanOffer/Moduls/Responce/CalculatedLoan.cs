using LoanOffer.Moduls.Classes;

namespace LoanOffer.Moduls.Responce
{
    public class CalculatedLoan
    {
        public List<ClientLoanInfo> ClientLoanInfo { get; set; }

        public CalculatedLoan()
        {
            ClientLoanInfo = new List<ClientLoanInfo>();
        }
    }
}
