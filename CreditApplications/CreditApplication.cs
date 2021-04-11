using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplications
{
    public class CreditApplication
    {
        // Make fields nullable for validation at [FromBody] binding

        [Required]
        [Range(0.0, double.PositiveInfinity, ErrorMessage = "Current amount must be a positive value.")]
        public double? CurrentAmount { get; set; }

        [Required]
        [Range(0.0, double.PositiveInfinity, ErrorMessage = "Requested amount must be a positive value.")]
        public double? RequestedAmount { get; set; }

        [Required]
        [Range(0.0, int.MaxValue, ErrorMessage = "Repayment amount must be a positive value.")]
        public int? RepaymentTerm { get; set; }
    }
}
