using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplications
{
    public class CreditApprovalRules
    {
        public double MinLoanAmount { get; set; }
        public double MaxLoanAmount { get; set; }
        public Dictionary<string, string> CurrentInterestRates { get; set; }

        public bool IsLoanAmountWithinBounds(double loanAmount)
        {
            return (MinLoanAmount <= loanAmount) && loanAmount <= MaxLoanAmount;
        }

        public double CalculateRateForAmount(double loanAmount)
        {
            if (CurrentInterestRates == null)
            {
                return 0;
            }
            double amountLowBound = 0;
            double interestRate = 0;

            var interestRates = new SortedDictionary<double, double>();

            foreach (var rate in CurrentInterestRates)
            {
                interestRates.Add(double.Parse(rate.Key), double.Parse(rate.Value));
            }

            foreach (var amountLimit in interestRates)
            {
                if (amountLowBound < loanAmount && loanAmount < amountLimit.Key)
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
