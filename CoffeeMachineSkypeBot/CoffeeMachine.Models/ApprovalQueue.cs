namespace CoffeeMachine.Models
{
	public class ApprovalQueue : EntitiyBase
	{
		public string UserName { get; set; }

		public bool Approved { get; set; }
	}
}
