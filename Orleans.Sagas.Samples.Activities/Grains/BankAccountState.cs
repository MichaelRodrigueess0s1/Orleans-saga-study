using System;
using System.Collections.Generic;

namespace Orleans.Sagas.Grains.Activities.Grains
{
	[GenerateSerializer]
	public class BankAccountState
	{
		[Id(0)]
		public int Balance { get; set; }
		[Id(1)]
		public Dictionary<Guid, int> Transactions { get; set; }

		public BankAccountState()
		{
			Transactions = new Dictionary<Guid, int>();
		}
	}
}