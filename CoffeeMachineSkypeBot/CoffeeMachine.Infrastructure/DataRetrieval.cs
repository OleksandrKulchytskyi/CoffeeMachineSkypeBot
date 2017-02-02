using System;
using CoffeeMachine.Abstraction;

namespace CoffeeMachine.Infrastructure
{
	public sealed class DataRetrieval : IDataService
	{
		public void AddActivity(string uid)
		{
			throw new NotImplementedException();
		}

		public void AddToApprovalQueue(string uid)
		{
			throw new NotImplementedException();
		}

		public int? Aggregate(string uid, AggregationType type)
		{
			return 1;
		}
	}
}
