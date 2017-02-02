using System;
using System.Linq;
using CoffeeMachine.Abstraction;

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

		public void AddToApprovalQueue(string uid)
		{
			var existed = dbContext.ApprovalQueue.FirstOrDefault(x => x.UserName == uid && !x.Approved);
			if (existed == null)
			{
				dbContext.ApprovalQueue.Add(new Models.ApprovalQueue { UserName = uid, Approved = false });
				dbContext.SaveChanges();
			}
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

		public void InitializeApprovedUsers()
		{
			var approved = dbContext.ApprovalQueue.Where(x => x.Approved)
													.Select(x => x.UserName)
													.ToArray();

			if (approved.Any())
			{
				DateTime createdOn = DateTime.UtcNow;
				foreach (var user in approved)
				{
					dbContext.Users.Add(new Models.User { Active = true, UserName = user, CreatedOn = createdOn });
					dbContext.SaveChanges();
				}
			}
		}
	}
}
