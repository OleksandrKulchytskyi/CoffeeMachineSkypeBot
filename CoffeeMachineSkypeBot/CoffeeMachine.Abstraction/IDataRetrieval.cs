namespace CoffeeMachine.Abstraction
{
	public interface IDataRetrieval
	{
		int? Aggregate(string uid, AggregationType type);
	}
}
