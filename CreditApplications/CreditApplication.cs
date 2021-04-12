using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditApplications
{
    [Serializable]
    public class CreditApplication : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.CurrentAmount,
                new ValidationContext(this, null, null) { MemberName = "CurrentAmount" },
                results);
            Validator.TryValidateProperty(this.RepaymentTerm,
                new ValidationContext(this, null, null) { MemberName = "RepaymentTerm" },
                results);
            Validator.TryValidateProperty(this.RequestedAmount,
            new ValidationContext(this, null, null) { MemberName = "RequestedAmount" },
            results);
            return results;
        }
    }
}
