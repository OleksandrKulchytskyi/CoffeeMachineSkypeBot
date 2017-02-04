﻿using CoffeeMachine.Abstraction;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;

namespace CoffeeMachineSkypeBot.Controllers
{
	public class CommandController : ApiController
	{
		private readonly ICommandHandler commandHandler;
		private readonly IDataService dataService;

		public CommandController(ICommandHandler commandHandler,
								IDataService dataService)
		{
			this.commandHandler = commandHandler;
			this.dataService = dataService;
		}

		[HttpGet]
		public HttpResponseMessage GetPendingApproval()
		{
			var users = dataService.GetUsersForApprove()
									.Select(x => new { Id = x.Id, UserId = x.UserId, UserName = x.UserName })
									.ToArray();
			return Request.CreateResponse(System.Net.HttpStatusCode.OK, users);
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

		[HttpPost]
		public HttpResponseMessage ApprovePending()
		{
			dataService.InitializeApprovedUsers();
			return Request.CreateResponse(System.Net.HttpStatusCode.OK);
		}

	}
}
