using CoffeeMachineSkypeBot.DTOs;
using System;
using System.Web.Http;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	public class AuthorizationController : ApiController
	{
		public AuthorizationController()
		{
		}

		[HttpPost]
		public IHttpActionResult Login(LoginInfo info)
		{
			if (info == null || String.IsNullOrEmpty(info.UserName) || String.IsNullOrEmpty(info.Password))
			{
				return this.BadRequest();
			}

			if (info.UserName == "admin" && info.Password == "coffee")
			{
				var authResponse = new AuthorizationResponse
				{
					Id = 1,
					UserName = "Admin",
					FirstName = "Administrator",
					LastName = String.Empty
				};

				return Ok(authResponse);
			}

			return Unauthorized();
		}
	}
}
