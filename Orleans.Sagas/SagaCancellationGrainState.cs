namespace Orleans.Sagas
{
	[GenerateSerializer]
	public class SagaCancellationGrainState
	{
		[Id(0)]
		public bool AbortRequested { get; set; }
	}
}