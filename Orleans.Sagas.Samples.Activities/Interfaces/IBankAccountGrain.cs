using System;
using System.Threading.Tasks;

namespace Orleans.Sagas.Grains.Activities.Interfaces
{
	public interface IBankAccountGrain : IGrainWithIntegerKey
	{
		Task<int> GetBalance();
		Task ModifyBalance(Guid transactionId, int amount);
		Task RevertBalanceModification(Guid transactionId);
	}
}
