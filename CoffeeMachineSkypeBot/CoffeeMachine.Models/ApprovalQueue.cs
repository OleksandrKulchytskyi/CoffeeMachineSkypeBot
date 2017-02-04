using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Models
{
	public class ApprovalQueue : EntitiyBase
	{
		[Required]
		[StringLength(100)]
		public string UserId { get; set; }

		[StringLength(100)]
		public string UserName { get; set; }

		public bool Approved { get; set; }
	}
}
