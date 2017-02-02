namespace CoffeeMachine.Abstraction
{
	public interface IDataService
	{
		int? Aggregate(string uid, AggregationType type);

		void AddToApprovalQueue(string uid);

		void AddActivity(string uid);
	}
}
