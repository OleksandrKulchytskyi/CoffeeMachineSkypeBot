using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Models
{
	public class UserActitvity : EntitiyBase
	{
		public DateTime Date { get; set; }

		public int Cups { get; set; }

		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}
