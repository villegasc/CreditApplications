using System.Collections.Generic;

namespace CreditApplications
{
    public class CreditApprovalRules
    {
        public double MinLoanAmount { get; set; }
        public double MaxLoanAmount { get; set; }

        // KV pairs represent (minimun_total_debt, interest_rate)
        // better with a separate class for clarity but kept as-is for simplicity
        public Dictionary<string, string> CurrentInterestRates { get; set; }

        public bool IsLoanAmountWithinBounds(double loanAmount)
        {
            return (MinLoanAmount <= loanAmount) && loanAmount <= MaxLoanAmount;
        }

        public double CalculateRateForAmount(double totalDebt)
        {
            if (CurrentInterestRates == null)
            {
                return 0;
            }
            double amountLowBound = 0;
            double interestRate = 0;

            SortedDictionary<double, double> interestRates = new SortedDictionary<double, double>();

            foreach (KeyValuePair<string, string> rate in CurrentInterestRates)
            {
                interestRates.Add(double.Parse(rate.Key), double.Parse(rate.Value));
            }

            foreach (KeyValuePair<double, double> amountLimit in interestRates)
            {
                if (amountLowBound <= totalDebt && totalDebt < amountLimit.Key)
                {
                    interestRate = amountLimit.Value;
                    break;
                }
                else
                {
                    amountLowBound = amountLimit.Key;
                }
            }
            return interestRate;
        }
    }
}