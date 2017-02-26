using CoffeeMachine.Abstraction;
using System.Linq;
using System.Web.Http;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	[RoutePrefix("api/pending")]
	public class PendingUsersController : ApiController
	{
		private readonly IDataService dataService;

		public PendingUsersController(IDataService dataService)
		{
			this.dataService = dataService;
		}

		[HttpGet]
		public IHttpActionResult GetAll()
		{
			var users = dataService.GetUsersForApprove()
									.Select(x => new DTOs.PendingUsersResponse { Id = x.Id, Identifier = x.UserId, UserName = x.UserName })
									.ToArray();
			return base.Ok(users);
		}

		[HttpPost]
		public IHttpActionResult ApprovePending()
		{
			dataService.InitializeApprovedUsers();
			return base.Ok();
		}
	}
}
