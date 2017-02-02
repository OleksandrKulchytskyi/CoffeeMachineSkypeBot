using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Models
{
	public class User : EntitiyBase
	{
		[StringLength(100)]
		public string UserName { get; set; }

		public DateTime CreatedOn { get; set; }

		public bool Active { get; set; }

		public virtual IList<UserActitvity> Activities { get; set; }
	}
}
