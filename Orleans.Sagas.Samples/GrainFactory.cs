using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Orleans.Sagas.Silo
{
	public abstract class GrainFactory
	{
		protected IGrainFactory Factory { get; private set; }
		protected ILogger<GrainFactory> Logger { get; }

		public GrainFactory(IGrainFactory grainFactory, ILogger<GrainFactory> logger)
		{
			Factory = grainFactory;
			Logger = logger;
		}

		public abstract Task Execute();
	}
}