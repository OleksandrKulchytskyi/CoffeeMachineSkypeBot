using CoffeeMachine.Abstraction;
using CoffeeMachine.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CoffeeMachine.Infrastructure
{
	public class CoffeeMachineContext : DbContext
	{
		private readonly IConnection connection;

		public CoffeeMachineContext() :
			base("name = CoffeeMachineConnection")
		{
		}

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

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Configurations.Add(new UserEntityConfiguration());
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class UserEntityConfiguration : EntityTypeConfiguration<User>
	{
		public UserEntityConfiguration()
		{
			this.ToTable("Users");

			this.HasKey<int>(s => s.Id);

			this.Property(p => p.UserIdentifier)
					.HasMaxLength(100).IsRequired();

			this.Property(p => p.UserDescription)
				.HasMaxLength(100).IsOptional();

			this.Property(p => p.CreatedOn).IsOptional();
		}
	}
}
