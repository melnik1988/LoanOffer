using LoanOffer.Moduls.Classes;

namespace LoanOffer.Interfaces
{
    public interface IHelp
    {
        ClientLoanInfo CreateClientLoanInfo(Client client, double price);
    }
}
