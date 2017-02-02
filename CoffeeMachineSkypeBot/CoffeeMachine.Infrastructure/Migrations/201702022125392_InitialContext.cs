namespace CoffeeMachine.Infrastructure.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class InitialContext : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.ApprovalQueue",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					UserName = c.String(maxLength: 100),
					Approved = c.Boolean(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.UserActitvity",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Date = c.DateTime(nullable: false),
					Cups = c.Int(nullable: false),
					UserId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.User",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					UserName = c.String(maxLength: 100),
					CreatedOn = c.DateTime(nullable: false),
					Active = c.Boolean(nullable: false),
				})
				.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropForeignKey("dbo.UserActitvity", "UserId", "dbo.User");
			DropIndex("dbo.UserActitvity", new[] { "UserId" });
			DropTable("dbo.User");
			DropTable("dbo.UserActitvity");
			DropTable("dbo.ApprovalQueue");
		}
	}
}
