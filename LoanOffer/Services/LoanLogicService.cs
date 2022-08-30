using LoanOffer.Interfaces;
using System.Linq.Expressions;

namespace LoanOffer.Services
{
    public class LoanLogicService: ILoadLogic
    {
        private IConfiguration configuration;
        private static double PrimeRate;
        private static double FixedRate;
        private static int MinMountsLoad;
        private static double MonthLimitRate;
        private static double returnPrice = 0;
        public LoanLogicService(IConfiguration _configuration)
        {
            configuration = _configuration;
            PrimeRate = Convert.ToDouble(configuration.GetSection("LoanLogic:Prime_Rate").Value);
            MinMountsLoad = Convert.ToInt16(configuration.GetSection("LoanLogic:Min_Mounts_Load").Value);
            MonthLimitRate = Convert.ToDouble(configuration.GetSection("LoanLogic:Month_Limit_Rate").Value);
        }

        private void RefreshRules()
        {
            FixedRate = 0;
            returnPrice = 0;
        } 
        public double LoadRules(int age, double[] requestingLoan, int periodInMonths)
        {
            RefreshRules();

            try
            {
                foreach (double rLoan in requestingLoan)
                {
                    if (age < 20)
                    {
                        FixedRate = 2;
                        returnPrice += LoanCalculation(rLoan, periodInMonths, true);
                    }
                    else if ((20 <= age) && (age <= 35))
                    {
                        if (rLoan <= 5000)
                        {
                            FixedRate = 2;
                            returnPrice += LoanCalculation(rLoan, periodInMonths, false);
                        }
                        else if ((rLoan > 5000) && (rLoan <= 30000))
                        {
                            FixedRate = 1.5;
                            returnPrice += LoanCalculation(rLoan, periodInMonths, true);
                        }
                        else if (rLoan > 30000)
                        {
                            FixedRate = 1;
                            returnPrice += LoanCalculation(rLoan, periodInMonths, true);
                        }
                    }
                    else if (age > 35)
                    {
                        if (rLoan <= 15000)
                        {
                            FixedRate = 1.5;
                            returnPrice += LoanCalculation(rLoan, periodInMonths, true);
                        }
                        else if ((rLoan > 15000) && (rLoan <= 45000))
                        {
                            FixedRate = 3;
                            returnPrice += LoanCalculation(rLoan, periodInMonths, true);
                        }
                        else if (rLoan > 45000)
                        {
                            FixedRate = 1;
                            returnPrice += LoanCalculation(rLoan, periodInMonths, false);
                        }
                    }
                }

                return returnPrice;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in LoadLogicService => LoadRules: " + ex);
            }
        }

        private double LoanCalculation(double requestingLoan, int periodInMonths, bool withPrime)
        {
            int overLimitMonths = 0;
            double rate;
            double overPrice;
            double overLimitMonthsPrice;
            double returnPrice;

            if ((periodInMonths - MinMountsLoad) > 0)
                overLimitMonths = periodInMonths - MinMountsLoad;

            if (withPrime)
            {
                rate = FixedRate + PrimeRate;
                
            }
            else
            {
                rate = FixedRate;
            }

            overPrice = requestingLoan / 100 * rate;
            overLimitMonthsPrice = requestingLoan / 100 * overLimitMonths * MonthLimitRate;
            returnPrice = requestingLoan + overPrice + overLimitMonthsPrice;

            returnPrice = Math.Round(returnPrice, 2);
            return returnPrice;
        }
    }
}
