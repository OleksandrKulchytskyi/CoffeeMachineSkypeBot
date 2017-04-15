using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Models;
using CoffeeMachine.Models;
using CoffeeMachine.Abstraction.Dto;

namespace CoffeeMachine.Infrastructure
{
	public sealed class SQLDataService : IDataService
	{
		private readonly CoffeeMachineContext context;

		public SQLDataService(CoffeeMachineContext context)
		{
			this.context = context;
		}

		public void AddActivity(string userIdentifier)
		{
			var user = context.Users.FirstOrDefault(x => x.UserIdentifier == userIdentifier);
			if (user != null)
			{
				user.Activities.Add(new UserActitvity { UserId = user.Id, Date = DateTime.UtcNow, Cups = 1 });
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
			DateTime till;
			DateTime from;

			switch (type)
			{
				case AggregationType.None:
					{
						return context.UserActivity.Where(x => x.UserId == user.Id).Count();
					}
				case AggregationType.Day:
					{
						till = now;
						from = now.AddHours(-24).Date;
						return context.UserActivity.AsNoTracking()
										.Where(x => x.UserId == user.Id && x.Date > from && x.Date < till)
										.Select(x => x.Date)
										.DefaultIfEmpty().Count();
					}
				case AggregationType.Month:
					{
						from = now.AddDays(-31);
						till = now;
						return context.UserActivity.AsNoTracking()
										.Where(x => x.UserId == user.Id && x.Date > from && x.Date < till)
										.Select(x => x.Date)
										.DefaultIfEmpty().Count();
					}
				case AggregationType.Year:
					{
						from = now.AddDays(-365);
						till = now;
						return context.UserActivity.AsNoTracking()
										.Where(x => x.UserId == user.Id && x.Date > from && x.Date < till)
										.Select(x => x.Date)
										.DefaultIfEmpty().Count();
					}
				default:
					{
						throw new NotImplementedException();
					}
			}
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
					context.Users.Add(new User
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
			var approved = context.ApprovalQueue.AsNoTracking()
												.Where(x => x.Approved)
												.Select(x => new { UserId = x.UserId, UserName = x.UserName })
												.ToList();

			if (approved.Count > 0)
			{
				DateTime createdOn = DateTime.UtcNow;
				foreach (var user in approved)
				{
					context.Users.Add(new User
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

		public async Task<List<ImportValidationResult>> ImportUserActivity(List<ImportDataContainer> importedData)
		{
			if (importedData == null || !importedData.Any())
			{
				return new List<ImportValidationResult>(1);
			}

			var validationResult = new List<ImportValidationResult>(importedData.Count);

			var userIdentifiers = importedData.Where(x => !String.IsNullOrEmpty(x.UserIdentifier))
											 .Select(x => x.UserIdentifier).Distinct()
											 .ToList();

			var dbUsers = context.Users.Where(x => userIdentifiers.Contains(x.UserIdentifier))
								.AsNoTracking().ToDictionary(x => x.UserIdentifier);

			foreach (var import in importedData)
			{
				if (String.IsNullOrEmpty(import.UserIdentifier))
				{
					validationResult.Add(new ImportValidationResult { RowId = import.RowId, Message = "User identifier cannot be empty." });
					continue;
				}

				if (!dbUsers.ContainsKey(import.UserIdentifier))
				{
					validationResult.Add(new ImportValidationResult { RowId = import.RowId, Message = "User identifier doesn't exists in the context." });
					continue;
				}

				if (dbUsers.ContainsKey(import.UserIdentifier) &&
					!dbUsers[import.UserIdentifier].Active)
				{
					validationResult.Add(new ImportValidationResult { RowId = import.RowId, Message = "User is disable in the system." });
					continue;
				}

				var user = dbUsers[import.UserIdentifier];
				context.UserActivity.Add(new UserActitvity { UserId = user.Id, Date = import.Date, Cups = 1 });
			}

			int procressed = await context.SaveChangesAsync();
			return validationResult;
		}
	}
}
