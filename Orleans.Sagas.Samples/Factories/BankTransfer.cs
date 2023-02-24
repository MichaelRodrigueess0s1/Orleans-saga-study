using Microsoft.Extensions.Logging;
using Orleans.Sagas.Grains.Activities;
using Orleans.Sagas.Grains.Activities.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orleans.Sagas.Silo.Factories
{
	public class BankTransfer : GrainFactory
	{
		public BankTransfer(IGrainFactory grainFactory, ILogger<GrainFactory> logger) : base(grainFactory, logger)
		{
		}

		public override async Task Execute()
		{
			await Factory.GetGrain<IBankAccountGrain>(1).ModifyBalance(Guid.Empty, 75);
			await Factory.GetGrain<IBankAccountGrain>(2).ModifyBalance(Guid.Empty, 75);
			await TransferAndWait(1, 2, 25);
			await TransferAndWait(2, 1, 20);
		}

		private async Task TransferAndWait(int from, int to, int amount)
		{
			var saga = await Transfer(from, to, amount);

			await saga.Wait();

			Logger.LogInformation("Account balances:");
			for (int accountId = 1; accountId <= 2; accountId++)
			{
				var account = Factory.GetGrain<IBankAccountGrain>(accountId);
				Logger.LogInformation($"  #{accountId} : {await account.GetBalance()}");
			}
		}

		private async Task<ISagaGrain> Transfer(int from, int to, int amount)
		{
			return await Factory.CreateSaga()
				.AddActivity<BalanceModificationActivity>
				(
					x =>
					{
						x.Add("Account", from);
						x.Add("Amount", -amount);
					}
				)
				.AddActivity<BalanceModificationActivity>
				(
					x =>
					{
						x.Add("Account", to);
						x.Add("Amount", amount);
					}
				)
				.ExecuteSagaAsync();
		}
	}
}
