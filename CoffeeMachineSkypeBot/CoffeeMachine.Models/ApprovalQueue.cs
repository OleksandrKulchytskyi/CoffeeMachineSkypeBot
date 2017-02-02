using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Models
{
	public class ApprovalQueue : EntitiyBase
	{
		[StringLength(100)]
		public string UserName { get; set; }

		public bool Approved { get; set; }
	}
}
