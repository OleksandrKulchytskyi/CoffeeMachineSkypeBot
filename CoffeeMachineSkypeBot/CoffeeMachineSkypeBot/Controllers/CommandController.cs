using CoffeeMachine.Abstraction;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

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
		public ResponseMessageResult Get(string uid)
		{
			dataService.InitializeApprovedUsers();

			var result = commandHandler.CanHandle(uid);
			var msg = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent($"Command - {uid} is operable - {result}")
			};
			return ResponseMessage(msg);
		}
	}
}
