using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CreditApplications
{
    public class CreditDecision
    {
        [JsonPropertyName("Approved")]
        public bool IsApproved { get; set; }

        [JsonPropertyName("InterestRate")]
        public double InterestRate { get; set; }

        public CreditDecision(CreditApplication cApplication, CreditApprovalRules aRules)
        {
            if (cApplication.GetType().GetProperties().All(p => p.GetValue(cApplication) != null))
            {
                var requestedAmount = (double)cApplication.RequestedAmount;
                var totalDebt = (double)(cApplication.RequestedAmount 
                                                + cApplication.CurrentAmount);

                IsApproved = aRules.IsLoanAmountWithinBounds(requestedAmount);
                if (IsApproved)
                {
                    InterestRate = aRules.CalculateRateForAmount(totalDebt);
                }
            }
        }
    }
}