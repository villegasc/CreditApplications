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
        public Dictionary<double, double> CurrentInterestRates { get; set; }
    }
}
