using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Models;

namespace CoffeeMachine.Infrastructure
{
	public sealed class DataRetrieval : IDataService
	{
		private readonly CoffeeMachineContext dbContext;

		public DataRetrieval(CoffeeMachineContext context)
		{
			dbContext = context;
		}

		public void AddActivity(string uid)
		{
			var user = dbContext.Users.FirstOrDefault(x => x.UserName == uid);
			if (user != null)
			{
				user.Activities.Add(new Models.UserActitvity { UserId = user.Id, Date = DateTime.UtcNow, Cups = 1 });
			}

			dbContext.SaveChanges();
		}

		public void AddUserForApprovalQueue(IEnumerable<AddUserRequest> members)
		{
			var userIds = members.Select(x => x.UserId).ToArray();
			var existed = dbContext.ApprovalQueue.Where(x => userIds.Contains(x.UserId))
												.ToArray();
			if (existed.Any())
			{
				foreach (var user in existed)
				{
					user.Approved = false;
				}
			}
			else
			{
				foreach (var user in members)
				{
					dbContext.ApprovalQueue.Add(new ApprovalQueue()
					{
						UserId = user.UserId,
						UserName = user.UserName,
						Approved = false
					});
				}
			}

			dbContext.SaveChanges();
		}

		public int? Aggregate(string uid, AggregationType type)
		{
			var user = dbContext.Users.FirstOrDefault(x => x.UserName == uid);
			if (user == null || !user.Active)
			{
				return 0;
			}

			DateTime now = DateTime.UtcNow;

			return dbContext.UserActivity.Where(x => x.UserId == user.Id).Count();
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

		public IEnumerable<ApprovalQueue> GetUsersForApprove()
		{
			dbContext.Configuration.AutoDetectChangesEnabled = false;

			var usersToApprove = dbContext.ApprovalQueue.Where(x => !x.Approved)
												  .ToArray();
			return usersToApprove;
		}

		public void InitializeApprovedUsers()
		{
			var approved = dbContext.ApprovalQueue.Where(x => x.Approved)
													.Select(x => new { UserId = x.UserId, UserName = x.UserName })
													.ToArray();

			if (approved.Any())
			{
				DateTime createdOn = DateTime.UtcNow;
				foreach (var user in approved)
				{
					dbContext.Users.Add(new Models.User
					{
						Active = true,
						UserName = user.UserId,
						UserDescription = user.UserName,
						CreatedOn = createdOn
					});
					dbContext.SaveChanges();
				}
			}
		}
	}
}
