using CoffeeMachine.Abstraction;

namespace CoffeeMachine.Infrastructure
{
	public sealed class DataRetrieval : IDataRetrieval
	{
		public int? Aggregate(string uid, AggregationType type)
		{
			return 1;
		}
	}
}
