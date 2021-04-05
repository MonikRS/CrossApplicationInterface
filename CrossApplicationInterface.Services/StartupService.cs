using CrossApplicationInterface.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrossApplicationInterface.Services
{
    public class StartupService : BackgroundService
    {
        private readonly ILogger<StartupService> _logger;
        private readonly IConfiguration _config;
        private readonly ProcessJobs _jobs;
        private System.Timers.Timer _timer;

        public StartupService(ILogger<StartupService> logger, IConfiguration config, ProcessJobs jobs)
        {
            _logger = logger;
            _config = config;
            _jobs = jobs;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application Starting @ {applicationStartTime}", DateTimeOffset.Now);
            _timer = new System.Timers.Timer
            {
                AutoReset = true,
                Enabled = true,
                Interval = 1000 * 5
            };
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application Stopped @ {applicationStopTime}", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer.Elapsed += TimerElapsed;
            await Task.Delay(1000 * 5, stoppingToken);
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogInformation("Timer Elapsed @ {timerElapsedTime}", DateTimeOffset.Now);           
            _jobs.ProcessScheduledJobs();
        }
    }
}
