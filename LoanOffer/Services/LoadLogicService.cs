using System.Linq.Expressions;

namespace LoanOffer.Services
{
    public class LoadLogicService
    {
        private IConfiguration configuration;
        private static double PrimeRate;
        private double FixedRate;
        private int MinMountsLoad;
        private double MonthLimitRate;
        public LoadLogicService(IConfiguration _configuration)
        {
            configuration = _configuration;
            PrimeRate = Convert.ToDouble(configuration.GetSection("LoanLogic:Prime_Rate"));
            MinMountsLoad = Convert.ToInt16(configuration.GetSection("LoanLogic:Min_Mounts_Load"));
            MonthLimitRate = Convert.ToDouble(configuration.GetSection("LoanLogic:Month_Limit_Rate"));
        }
        public void LoadRules(int age, double requestingLoan, int periodInMonths)
        {
            try
            {
                if (age < 20)
                {
                    FixedRate = 2;
                    LoanCalculation(periodInMonths, true);
                }
                else if ((20 <= age) && (age <= 35))
                {
                    if (requestingLoan <= 5000)
                    {
                        FixedRate = 2;
                        LoanCalculation(periodInMonths, false);
                    }
                    else if ((requestingLoan > 5000) && (requestingLoan <= 30000))
                    {
                        FixedRate = 1.5;
                        LoanCalculation(periodInMonths, true);
                    }
                    else if (requestingLoan > 30000)
                    {
                        FixedRate = 1;
                        LoanCalculation(periodInMonths, true);
                    }
                }
                else if (age > 35)
                {
                    if (requestingLoan <= 15000)
                    {
                        FixedRate = 1.5;
                        LoanCalculation(periodInMonths, true);
                    }
                    else if ((requestingLoan > 15000) && (requestingLoan <= 45000))
                    {
                        FixedRate = 3;
                        LoanCalculation(periodInMonths, true);
                    }
                    else if (requestingLoan > 45000)
                    {
                        FixedRate = 1;
                        LoanCalculation(periodInMonths, false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in LoadLogicService => LoadRules: " + ex);
            }
        }

        private void LoanCalculation(int periodInMonths, bool withPrime)
        {
            int overLimitMonths = 0;
            double rate = 0;

            if ((periodInMonths - MinMountsLoad) > 0)
                overLimitMonths = periodInMonths - MinMountsLoad;

            if (withPrime)
            {
                rate = PrimeRate + FixedRate;
                rate = Math.Round(rate, 2);



            }
            else
            {

            }
        }
    }
}
