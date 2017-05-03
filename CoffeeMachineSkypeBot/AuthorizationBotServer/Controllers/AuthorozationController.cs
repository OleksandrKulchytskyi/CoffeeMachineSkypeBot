using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthorizationBotServer.Models;

namespace AuthorizationBotServer.Controllers
{
	[Route("api/auth")]
	public class ValuesController : Controller
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(new TokenResponse { Token = "Yeah" });
		}

		[HttpPost]
		public IActionResult Post([FromBody] LoginModel model)
		{
			if (model == null || !ModelState.IsValid)
			{
				return BadRequest();
			}

			return Ok(new TokenResponse { Token = "Yeah" });
		}
	}
}
