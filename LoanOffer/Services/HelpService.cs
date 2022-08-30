using LoanOffer.Interfaces;
using LoanOffer.Moduls.Classes;

namespace LoanOffer.Services
{
    public class HelpService:IHelp
    { 
        public HelpService()
        {
        }

        public ClientLoanInfo CreateClientLoanInfo(Client client, double price)
        {
            ClientLoanInfo clientLoanInfo = new ClientLoanInfo();

            clientLoanInfo.ClientId = client.Id;
            clientLoanInfo.LoanId = client.Id + client.Age;
            clientLoanInfo.LoanPrice = price;

            return clientLoanInfo;
        }
    }
}
