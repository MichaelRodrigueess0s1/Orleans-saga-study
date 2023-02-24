using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Sagas.Silo.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace Orleans.Sagas.Silo
{
	class Runner : IHostedService
	{
		private readonly BankTransfer BankTransfer;
		private readonly ILogger<Runner> Logger;

		public Runner(
			BankTransfer bankTransferSample,
			ILogger<Runner> logger)
		{
			BankTransfer = bankTransferSample;
			Logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Logger.LogDebug($"Running  '{BankTransfer.GetType().Name}'...");
			await BankTransfer.Execute();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
