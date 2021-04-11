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
        private CreditApprovalRules _CApprovalRules { get; set; }

        public CreditDecision(CreditApplication cApplication, CreditApprovalRules aRules) 
        {
            _CApplication = cApplication;
            _CApprovalRules = aRules;
            ProcessRequest();
        }

        private void ProcessRequest()
        {
            throw new NotImplementedException();
        }
    }
}