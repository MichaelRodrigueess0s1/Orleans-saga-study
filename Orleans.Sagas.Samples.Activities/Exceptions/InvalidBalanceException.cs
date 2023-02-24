using System;
using System.Runtime.Serialization;

namespace Orleans.Sagas.Grains.Exceptions
{
	[Serializable]
	internal class InvalidBalanceException : Exception
	{
		public InvalidBalanceException()
		{
		}

		public InvalidBalanceException(string message) : base(message)
		{
		}

		public InvalidBalanceException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}