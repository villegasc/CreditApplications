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

        public CreditDecision(CreditApplication cApplication, CreditApprovalRules aRules) 
        {
            if (cApplication.GetType().GetProperties().All(p => p.GetValue(cApplication) != null))
            {
                IsApproved = aRules.IsLoanAmountWithinBounds(cApplication.RequestedAmount);
                InterestRate = aRules.CalculateRateForAmount(cApplication.RequestedAmount + cApplication.CurrentAmount);
            }
        }
    }
}