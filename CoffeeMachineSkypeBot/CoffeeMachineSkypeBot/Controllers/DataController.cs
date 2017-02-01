using CoffeeMachine.Abstraction;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace CoffeeMachineSkypeBot.Controllers
{
	public class DataController : ApiController
	{
		private readonly IDataRetrieval dataService;

		public DataController(IDataRetrieval dalService)
		{
			dataService = dalService;
		}

		[HttpGet]
		public ResponseMessageResult Get(string uid)
		{
			var result = dataService.Aggregate(uid, AggregationType.None);
			var msg = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent($"User - {uid} with result- {result}")
			};
			return ResponseMessage(msg);
		}
}
}
