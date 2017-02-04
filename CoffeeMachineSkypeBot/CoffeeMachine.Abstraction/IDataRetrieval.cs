using CoffeeMachine.Models;
using System.Collections.Generic;

namespace CoffeeMachine.Abstraction
{
	public interface IDataService
	{
		void AddActivity(string uid);

		int? Aggregate(string uid, AggregationType type);

		void AddUserForApprovalQueue(IEnumerable<AddUserRequest> members);

		void InitializeApprovedUsers();

		IEnumerable<ApprovalQueue> GetUsersForApprove();
	}
}
