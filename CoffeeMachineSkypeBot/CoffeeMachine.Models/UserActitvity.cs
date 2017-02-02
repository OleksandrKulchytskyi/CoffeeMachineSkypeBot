using System;
namespace CoffeeMachine.Models
{
	public class UserActitvity : EntitiyBase
	{
		public DateTime Date { get; set; }

		public string UserName { get; set; }

		public int Cups { get; set; }
	}
}
