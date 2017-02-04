using CoffeeMachine.Abstraction.Models;
using CoffeeMachine.Models;
using System.Collections.Generic;

namespace CoffeeMachine.Abstraction
{
	public interface IDataService
	{
		void AddActivity(string uid);

		int? Aggregate(string uid, AggregationType type);

		void AddUserForApprovalQueue(IEnumerable<AddUserRequest> members);

		UserStatus CheckUserStatus(string uid);

		void InitializeApprovedUsers();

		IEnumerable<ApprovalQueue> GetUsersForApprove();
	}
}
