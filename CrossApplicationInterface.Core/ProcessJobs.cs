using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace CrossApplicationInterface.Core
{
    public class ProcessJobs
    {
        private readonly ILogger<ProcessJobs> _logger;
        private readonly IConfiguration _config;

        public ProcessJobs(ILogger<ProcessJobs> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void ProcessScheduledJobs()
        {
            _logger.LogInformation("Starting to Process Jobs @ {processStartTime}", DateTimeOffset.Now);
        }
    }
}
