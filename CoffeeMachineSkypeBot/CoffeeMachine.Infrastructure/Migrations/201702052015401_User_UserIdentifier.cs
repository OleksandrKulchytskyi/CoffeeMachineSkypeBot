namespace CoffeeMachine.Infrastructure.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class User_UserIdentifier : DbMigration
	{
		public override void Up()
		{
			RenameColumn("dbo.User", "UserName", "UserIdentifier");
		}

		public override void Down()
		{
			RenameColumn("dbo.User", "UserIdentifier", "UserName");
		}
	}
}
