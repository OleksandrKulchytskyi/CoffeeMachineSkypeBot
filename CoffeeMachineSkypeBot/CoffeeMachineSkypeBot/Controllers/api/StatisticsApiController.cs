using CoffeeMachine.Abstraction;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	//[Authorize]
	public class StatisticsApiController : ApiController
	{
		private readonly IDataService dataService;

		public StatisticsApiController(IDataService dataService)
		{
			this.dataService = dataService;
		}

		[HttpPost]
		public async Task<IHttpActionResult> Upload()
		{
			var stream = await Request.Content.ReadAsStreamAsync();
			using (StreamReader sr = new StreamReader(stream))
			{
				string content = sr.ReadToEnd();
			}

			return Ok();
		}
	}
}
