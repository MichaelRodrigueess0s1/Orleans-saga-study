using Orleans.Sagas.Grains.Activities.Interfaces;
using Orleans.Sagas.Grains.Exceptions;
using System;
using System.Threading.Tasks;


namespace Orleans.Sagas.Grains.Activities.Grains
{
	public class BankAccountGrain : Grain<BankAccountState>, IBankAccountGrain
	{
		public Task<int> GetBalance()
		{
			return Task.FromResult(State.Balance);
		}

		public async Task ModifyBalance(Guid transactionId, int amount)
		{
			if (State.Transactions.ContainsKey(transactionId))
			{
				return;
			}

			var newBalance = State.Balance + amount;

			if (newBalance >= 0 &&
				newBalance <= 100)
			{
				State.Balance += amount;
				State.Transactions[transactionId] = amount;
				await WriteStateAsync();
				return;
			}

			throw new InvalidBalanceException();
		}

		public async Task RevertBalanceModification(Guid transactionId)
		{
			if (State.Transactions.ContainsKey(transactionId))
			{
				if (State.Transactions[transactionId] == 0)
				{
					return;
				}

				State.Balance -= State.Transactions[transactionId];
			}

			State.Transactions[transactionId] = 0;

			await WriteStateAsync();
		}
	}
}
