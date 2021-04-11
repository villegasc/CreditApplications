using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplications
{
    public class CreditApplication
    {
        public double CurrentAmount { get; set; }
        public double RequestedAmount { get; set; }
        public int RepaymentTerm { get; set; }
    }
}
