using CreditApplications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CreditApplicationTests
{
    [TestClass]
    public class CreditApplicationTest
    {
        // interest rates pairs represent (minimum_total_debt, interest_rate)
        // array of InterestRate class would have been better for clarity

        // ASUMMED: total future debt ranges in original task are a mistake,
        // as they leave ot ranges [39000 - 40000] and [59000 - 60000]

        public static string approvalCriteriaStr = @"{""MinLoanAmount"": 2000, 
                                            ""MaxLoanAmount"": 69000, 
                                            ""CurrentInterestRates"": 
                                                        {""0"": 3,
                                                        ""20000"": 4,
                                                        ""40000"": 5,
                                                        ""60000"": 6}}";

        public static CreditApprovalRules approvalCriteria = JsonConvert.DeserializeObject<CreditApprovalRules>(approvalCriteriaStr);

        [TestMethod]
        public void ValidateCreditApplication_WellFormed()
        {
            // all fields present
            CreditApplication creditApplication = new CreditApplication
            {
                RequestedAmount = 20000,
                CurrentAmount = 1000,
                RepaymentTerm = 12
            };
            IEnumerable<ValidationResult> creditApplicationValidation = creditApplication.Validate(new ValidationContext(creditApplication));
            Assert.AreEqual(creditApplicationValidation.ToList().Count, 0);
        }

        [TestMethod]
        public void ValidateCreditApplication_MissingFields()
        {
            // 2 fields not present
            CreditApplication creditApplication = new CreditApplication 
            { 
                RequestedAmount = 20000 
            };
            IEnumerable<ValidationResult> creditApplicationValidation = creditApplication.Validate(new ValidationContext(creditApplication));
            Assert.IsNotNull(creditApplicationValidation);
            Assert.AreEqual(creditApplicationValidation.ToList().Count, 2);
            Assert.IsTrue(creditApplicationValidation.ToList().
                            Exists(p => string.Equals(p.ErrorMessage, "The CurrentAmount field is required.")));
        }

        [TestMethod]
        public void ValidateCreditApplication_NegativeValues()
        {
            CreditApplication creditApplication = new CreditApplication
            {
                RequestedAmount = 20000, RepaymentTerm = 10, CurrentAmount = -200
            };
            IEnumerable<ValidationResult> creditApplicationValidation = creditApplication.Validate(new ValidationContext(creditApplication));
            Assert.IsNotNull(creditApplicationValidation);
            Assert.AreEqual(creditApplicationValidation.ToList().Count, 1);
            Assert.IsTrue(creditApplicationValidation.ToList().
                            Exists(p => string.Equals(p.ErrorMessage, "Current amount must be a positive value.")));
        }

        [TestMethod]
        public void Test_CreditApplication_Rejected()
        {
            // Should be rejected if amount <2000 and >69000
            string request_1999 = "{\"CurrentAmount\": 21000, \"RepaymentTerm\": 12, \"RequestedAmount\": 1999}";
            string request_69001 = "{\"CurrentAmount\": 21000, \"RepaymentTerm\": 12, \"RequestedAmount\": 69001}";
            CreditApplication cRequest_1999 = JsonConvert.DeserializeObject<CreditApplication>(request_1999);
            CreditApplication cRequest_69001 = JsonConvert.DeserializeObject<CreditApplication>(request_69001);

            Assert.IsFalse((new CreditDecision(cRequest_1999, approvalCriteria).IsApproved));
            Assert.IsFalse((new CreditDecision(cRequest_69001, approvalCriteria).IsApproved));
        }

        [TestMethod]
        public void Test_CreditApplication_Approved()
        {
            // Should be approved  - assumed 2000 is an approved value (but it's not clear in this task)
            string request_2000 = "{\"CurrentAmount\": 21000, \"RepaymentTerm\": 12, \"RequestedAmount\": 2000}";
            string request_69000 = "{\"CurrentAmount\": 21000, \"RepaymentTerm\": 12, \"RequestedAmount\": 69000}";

            CreditApplication cRequest_2000 = JsonConvert.DeserializeObject<CreditApplication>(request_2000);
            CreditApplication cRequest_69000 = JsonConvert.DeserializeObject<CreditApplication>(request_69000);

            Assert.IsTrue((new CreditDecision(cRequest_2000, approvalCriteria).IsApproved));
            Assert.IsTrue((new CreditDecision(cRequest_69000, approvalCriteria).IsApproved));
        }

        [TestMethod]
        public void Test_InterestRate_For_Rejected_Application()

        {
            // check approval rate for a valid, but out of loan range request, assumed to be 0
            string request_rejected = "{\"CurrentAmount\": 17999, \"RepaymentTerm\": 12, \"RequestedAmount\": 500000}";
            CreditApplication cRequest_rejected = JsonConvert.DeserializeObject<CreditApplication>(request_rejected);
            Assert.AreEqual((new CreditDecision(cRequest_rejected, approvalCriteria)).InterestRate, 0);
        }

        public void Test_InterestRate_For_Approved_Application()
        {
            // Total debt: 20000, Expected 4
            string request_up_to_40000 = "{\"CurrentAmount\": 21000, \"RepaymentTerm\": 12, \"RequestedAmount\": 2000}";
            CreditApplication cRequest_up_to_40000 = JsonConvert.DeserializeObject<CreditApplication>(request_up_to_40000);
            Assert.AreEqual((new CreditDecision(cRequest_up_to_40000, approvalCriteria)).InterestRate, 4);

            // Total debt: 60000, Expected 5
            string request_up_to_60000 = "{\"CurrentAmount\": 17999, \"RepaymentTerm\": 12, \"RequestedAmount\": 2000}";
            CreditApplication cRequest_up_to_60000 = JsonConvert.DeserializeObject<CreditApplication>(request_up_to_60000);
            Assert.AreEqual((new CreditDecision(cRequest_up_to_60000, approvalCriteria)).InterestRate, 5);

            // Total debt: 60000.1, Expected 6
            string request_plus_60000 = "{\"CurrentAmount\": 17999, \"RepaymentTerm\": 12, \"RequestedAmount\": 2000}";
            CreditApplication cRequest_plus_60000 = JsonConvert.DeserializeObject<CreditApplication>(request_plus_60000);
            Assert.AreEqual((new CreditDecision(cRequest_plus_60000, approvalCriteria)).InterestRate, 6);
        }
    }
}