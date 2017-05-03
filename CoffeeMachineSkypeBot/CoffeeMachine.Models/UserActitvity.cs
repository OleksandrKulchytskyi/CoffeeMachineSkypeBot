using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Models
{
	[Table("UserActitvity")]
	public class UserActivity : EntitiyBase
	{
		public DateTime Date { get; set; }

		public int Cups { get; set; }

		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}
