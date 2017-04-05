namespace CoffeeMachine.Abstraction.Models
{
	public sealed class PendingUsersResponse
	{
		public int Id { get; set; }
		public string Identifier { get; set; }
		public string UserName { get; set; }
	}
}