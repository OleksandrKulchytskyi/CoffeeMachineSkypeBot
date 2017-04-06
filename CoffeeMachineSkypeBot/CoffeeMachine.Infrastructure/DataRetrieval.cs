using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Models;
using CoffeeMachine.Models;

namespace CoffeeMachine.Infrastructure
{
	public sealed class DataRetrieval : IDataService
	{
		private readonly CoffeeMachineContext context;

		public DataRetrieval(CoffeeMachineContext context)
		{
			this.context = context;
		}

		public void AddActivity(string uid)
		{
			var user = context.Users.FirstOrDefault(x => x.UserIdentifier == uid);
			if (user != null)
			{
				user.Activities.Add(new Models.UserActitvity { UserId = user.Id, Date = DateTime.UtcNow, Cups = 1 });
				context.SaveChanges();
			}
		}

		public void AddUserForApprovalQueue(IEnumerable<AddUserRequest> members)
		{
			var userIds = members.Select(x => x.UserId).ToArray();
			var existed = context.ApprovalQueue.Where(x => userIds.Contains(x.UserId))
												.ToArray();
			if (existed.Any())
			{
				foreach (var user in existed)
				{
					user.Approved = false;
				}
			}

			if (existed.Count() != members.Count())
			{
				foreach (var user in members)
				{
					context.ApprovalQueue.Add(new ApprovalQueue()
					{
						UserId = user.UserId,
						UserName = user.UserName,
						Approved = false
					});
				}
			}

			context.SaveChanges();
		}

		public int? Aggregate(string uid, AggregationType type)
		{
			var user = context.Users.FirstOrDefault(x => x.UserIdentifier == uid);
			if (user == null || !user.Active)
			{
				return 0;
			}

			DateTime now = DateTime.UtcNow;

			return context.UserActivity.Where(x => x.UserId == user.Id).Count();
			//switch (type)
			//{
			//	case AggregationType.None:
			//		return 0;
			//		break;
			//	case AggregationType.Day:
			//		var till = now.AddHours(-24).Date;
			//		return dbContext.UserActivity.Where(x => x.UserId == user.Id && x.Date > till).DefaultIfEmpty(0).Count();
			//                 break;
			//	case AggregationType.Month:
			//		break;
			//	case AggregationType.Year:
			//		break;
			//	default:
			//		break;
			//}
		}

		public UserStatus CheckUserStatus(string uid)
		{
			var approvedUser = context.Users.FirstOrDefault(x => x.UserIdentifier == uid);
			if (approvedUser != null)
			{
				return approvedUser.Active ? UserStatus.Active : UserStatus.Inactive;
			}

			var pending = context.ApprovalQueue.FirstOrDefault(x => x.UserId.Equals(uid));
			return pending != null ? UserStatus.PendindApproval : UserStatus.Unknown;
		}

		public IEnumerable<ApprovalQueue> GetUsersForApprove()
		{
			context.Configuration.AutoDetectChangesEnabled = false;

			var usersToApprove = context.ApprovalQueue.Where(x => !x.Approved)
												  .ToArray();
			return usersToApprove;
		}

		public void ApproveUsers(IEnumerable<int> ids)
		{
			var forApproval = context.ApprovalQueue.Where(x => ids.Contains(x.Id))
												.ToList();

			if (forApproval.Any())
			{
				DateTime createdOn = DateTime.UtcNow;
				foreach (var user in forApproval)
				{
					context.Users.Add(new Models.User
					{
						Active = true,
						UserIdentifier = user.UserId,
						UserDescription = user.UserName,
						CreatedOn = createdOn
					});
				}

				forApproval.ForEach(i => context.ApprovalQueue.Remove(i));

				context.SaveChanges();
			}
		}

		public void InitializeApprovedUsers()
		{
			var approved = context.ApprovalQueue.Where(x => x.Approved)
													.Select(x => new { UserId = x.UserId, UserName = x.UserName })
													.ToArray();

			if (approved.Any())
			{
				DateTime createdOn = DateTime.UtcNow;
				foreach (var user in approved)
				{
					context.Users.Add(new Models.User
					{
						Active = true,
						UserIdentifier = user.UserId,
						UserDescription = user.UserName,
						CreatedOn = createdOn
					});
					context.SaveChanges();
				}
			}
		}
	}
}
