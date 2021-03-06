﻿using CoffeeMachine.Abstraction;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace CoffeeMachineSkypeBot.Controllers
{
	[System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "get")]
	public class CommandController : ApiController
	{
		private readonly ICommandHandler commandHandler;

		public CommandController(ICommandHandler commandHandler)
		{
			this.commandHandler = commandHandler;
		}

		[HttpGet]
		public ResponseMessageResult Get(string uid)
		{
			var result = commandHandler.CanHandle(uid);
			var msg = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent($"Command - {uid} is operable - {result}")
			};
			return ResponseMessage(msg);
		}
	}
}
