namespace CoffeeMachine.Abstraction
{
	public interface IDataService
	{
		void AddActivity(string uid);

		int? Aggregate(string uid, AggregationType type);

		void AddToApprovalQueue(string uid);

		void InitializeApprovedUsers();
	}
}
