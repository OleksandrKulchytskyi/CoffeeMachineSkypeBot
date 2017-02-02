using System;

namespace CoffeeMachine.Models
{
	public class UserActitvity : EntitiyBase
	{
		public int UserId { get; set; }

		public DateTime Date { get; set; }

		public int Cups { get; set; }

		public virtual User User { get; set; }
	}
}
