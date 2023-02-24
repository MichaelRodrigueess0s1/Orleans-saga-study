using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Sagas.Grains.Activities;
using Orleans.Sagas.Samples.Activities;
using Orleans.Sagas.Samples.Examples;
using Orleans.Sagas.Silo.Factories;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orleans.Sagas.Silo
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await Host.CreateDefaultBuilder(args)
				.UseOrleans(siloBuilder =>
				{
					siloBuilder
						.UseLocalhostClustering()
						.UseSagas(typeof(BalanceModificationActivity).Assembly)
						.ConfigureLogging(logging =>
						{
							logging.AddConsole();
						})
						.Configure<ClusterOptions>(opts =>
						{
							opts.ClusterId = nameof(Sagas);
							opts.ServiceId = nameof(Sagas);
						})
						.Configure<EndpointOptions>(opts =>
						{
							opts.AdvertisedIPAddress = IPAddress.Loopback;
						})
						.ConfigureServices(services =>
						{
							services.AddSingleton<HttpClient>();

							services.AddTransient<BankTransfer>();

							services.AddHostedService<SampleRunner>();
						})
					   .AddMemoryGrainStorageAsDefault()
					   .UseInMemoryReminderService();
				})
				.RunConsoleAsync();
		}
	}
}
