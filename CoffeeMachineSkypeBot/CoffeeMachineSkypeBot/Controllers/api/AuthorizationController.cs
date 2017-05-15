using System;
using System.Web.Http;
using CoffeeMachineSkypeBot.DTOs;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	[System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
	public class AuthorizationController : ApiController
	{
		public AuthorizationController()
		{
		}

		[Route("api/auth")]
		[HttpPost]
		public IHttpActionResult Login(LoginInfo info)
		{
			if (info == null || 
				String.IsNullOrEmpty(info.UserName) ||
				String.IsNullOrEmpty(info.Password))
			{
				return this.BadRequest();
			}

			if (info.UserName.Equals("admin", StringComparison.OrdinalIgnoreCase) && 
				info.Password.Equals("coffee",StringComparison.OrdinalIgnoreCase))
			{
				var authResponse = new AuthorizationResponse
				{
					Id = 1,
					UserName = "Admin",
					FirstName = "Administrator",
					LastName = String.Empty,
					Token = "fake-jwt-token"
				};

				return Ok<AuthorizationResponse>(authResponse);
			}

			return Unauthorized();
		}
	}
}
