using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditApplications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditApplicationController : ControllerBase
    {
        private IConfiguration _Config;
        private CreditApprovalRules _CreditApprovalRules;

        public CreditApplicationController(IConfiguration Configuration)
        {
            _Config = Configuration;
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
