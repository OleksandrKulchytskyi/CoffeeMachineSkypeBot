using CoffeeMachine.Abstraction;
using CoffeeMachine.Models;
using System.Data.Entity;

namespace CoffeeMachine.Infrastructure
{
	public class CoffeeMachineContext : DbContext
	{
		private readonly IConnection connection;
		public CoffeeMachineContext(IConnection connection) :
			base(connection.ConnectionText)
		{
			this.connection = connection;
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;
		}

		public IDbSet<ApprovalQueue> ApprovalQueue { get; set; }

		public IDbSet<User> Users { get; set; }

		public IDbSet<UserActitvity> UserActivity { get; set; }
	}
}
