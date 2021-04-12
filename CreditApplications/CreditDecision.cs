using System.Linq;
using System.Text.Json.Serialization;

namespace CreditApplications
{
    public class CreditDecision
    {
        [JsonPropertyName("Approved")]
        public bool IsApproved { get; set; } = false;

        [JsonPropertyName("InterestRate")]
        public double InterestRate { get; set; } = 0;

        public CreditDecision(CreditApplication cApplication, CreditApprovalRules aRules)
        {
            if (cApplication.GetType().GetProperties().All(p => p.GetValue(cApplication) != null))
            {
                double requestedAmount = (double)cApplication.RequestedAmount;
                double totalDebt = (double)(cApplication.RequestedAmount 
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