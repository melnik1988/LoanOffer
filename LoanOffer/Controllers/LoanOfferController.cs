using LoanOffer.Interfaces;
using LoanOffer.Moduls.Classes;
using LoanOffer.Moduls.Request;
using LoanOffer.Moduls.Responce;
using LoanOffer.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace LoanOffer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanOfferController : ControllerBase
    {
        private ILoadLogic logicService;
        private IHelp helpService;
        private IFile fileService;

        private CalculatedLoan calculatedLoan;
        public LoanOfferController(ILoadLogic _LogicService, IHelp _helpService, IFile _fileService)
        {
            logicService = _LogicService;
            helpService = _helpService;
            fileService = _fileService;
        }


        [HttpPost("GetLoans")]
        public async Task<CalculatedLoan> GetLoans([FromBody]LoanApplication loanApplication)
        {
            calculatedLoan = new CalculatedLoan();

            if (loanApplication == null)
                throw new Exception("Error: LoanOfferController -> GetLoans -> nullable value");

            try
            {
                // Parallel
                var tasks = new List<Task>();
                foreach (var client in loanApplication.Clients)
                {
                    tasks.Add(Task.Run( () =>
                    {
                        CalculateLoan(client, calculatedLoan);
                    }));

                    await Task.WhenAll(tasks);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in LoanOfferController -> GetLoans", ex);
            }

            return calculatedLoan;
        }

        [HttpGet("GetLoansFromLocalFileJson")]
        public async Task<CalculatedLoan> GetLoansFromFileJson()
        {
            calculatedLoan = new CalculatedLoan();

            string json = fileService.ReadFromFile("loanrequest");
            LoanApplication loanApplication = JsonConvert.DeserializeObject<LoanApplication>(json);

            if (loanApplication == null)
                throw new Exception("Error: LoanOfferController -> GetLoans -> nullable loanApplication");

            try
            {
                // Parallel
                var tasks = new List<Task>();
                foreach (var client in loanApplication.Clients)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        CalculateLoan(client, calculatedLoan);
                    }));

                    await Task.WhenAll(tasks);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Exception in LoanOfferController -> GetLoans", ex);
            }

            return calculatedLoan;
        }

        private void CalculateLoan(Client client, CalculatedLoan calculatedLoan)
        {
            double price = logicService.LoadRules(client.Age, client.RequestingLoan, client.PeriodInMonths);

            calculatedLoan.ClientLoanInfo.Add(helpService.CreateClientLoanInfo(client, price));
        }

    }
}
