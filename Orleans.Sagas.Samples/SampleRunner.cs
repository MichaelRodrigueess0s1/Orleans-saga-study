using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Sagas.Silo.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace Orleans.Sagas.Silo
{
	class SampleRunner : IHostedService
	{
		private readonly BankTransfer BankTransfer;
		private readonly ILogger<SampleRunner> logger;

		public SampleRunner(
			BankTransfer bankTransferSample,
			ILogger<SampleRunner> logger)
		{
			this.BankTransfer = bankTransferSample;
			this.logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{

			logger.LogDebug($"Running  '{BankTransfer.GetType().Name}'...");
			await BankTransfer.Execute();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
