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

        private SortedDictionary<double, double> _CurrentInterestRatesParsed = new SortedDictionary<double, double>();

        public SortedDictionary<double, double> GetCurrentInterestRates()
        {
            if (CurrentInterestRates == null)
            {
                return null;
            }
            foreach (KeyValuePair<string, string> rate in CurrentInterestRates)
            {
                _CurrentInterestRatesParsed.Add(double.Parse(rate.Key), double.Parse(rate.Value));
            }
            return _CurrentInterestRatesParsed;
        }
    }

}
