using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationBotServer.Models
{
    public class LoginModel
    {
		[Required]
		public string User { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
