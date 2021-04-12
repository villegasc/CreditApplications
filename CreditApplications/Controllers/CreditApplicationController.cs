using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace CreditApplications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditApplicationController : ControllerBase
    {
        private IConfiguration _Config;
        private CreditApprovalRules _CreditApprovalRules = new CreditApprovalRules();

        public CreditApplicationController(IConfiguration Configuration)
        {
            _Config = Configuration;

            //For simplicity, we read approval rules from config
            _Config.GetSection("ApprovalCriteria").Bind(_CreditApprovalRules);

            if (_CreditApprovalRules.CurrentInterestRates == null)
            {
                throw new ConfigurationErrorsException("Could not read approval rules from configuration.");
            }
        }

        [HttpGet("version")]
        public string Get()
        {
            return string.Format("Running credit app version {0}",
                    typeof(CreditApplication).Assembly.GetName().Version.ToString());
        }

        [HttpPost("evaluate")]
        public CreditDecision EvaluateCreditApplication([FromBody] CreditApplication creditApplication)
        {
            return new CreditDecision(creditApplication, _CreditApprovalRules);
        }
    }
}
