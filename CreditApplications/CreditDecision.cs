using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplications
{
    public class CreditDecision
    {
        public bool IsApproved { get; set; }
        public double InterestRate { get; set; }
        private CreditApplication _CApplication { get; set; }
    }
}