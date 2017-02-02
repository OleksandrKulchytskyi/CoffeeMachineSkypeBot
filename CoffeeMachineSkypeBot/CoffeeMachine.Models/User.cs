using System;
using System.Collections.Generic;

namespace CoffeeMachine.Models
{
	public class User : EntitiyBase
	{
		public string UserName { get; set; }

		public DateTime CreatedOn { get; set; }

		public bool Active { get; set; }

		public virtual IList<UserActitvity> Activities { get; set; }
	}
}
