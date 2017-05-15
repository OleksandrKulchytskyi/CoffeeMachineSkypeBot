using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Models;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	[System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
	[RoutePrefix("api/pending")]
	public class PendingUsersController : ApiController
	{
		private readonly IDataService dataService;

		public PendingUsersController(IDataService dataService)
		{
			this.dataService = dataService;
		}

		[Route("getall")]
		[HttpGet]
		public IHttpActionResult GetAll()
		{
			var users = dataService.GetUsersForApprove()
									.Select(x => new PendingUsersResponse { Id = x.Id, Identifier = x.UserId, UserName = x.UserName })
									.ToArray();
			return base.Ok(users);
		}

		[Route("byids")]
		[HttpPost]
		public IHttpActionResult ApproveByIds(IEnumerable<int> ids)
		{
			if (ids == null || !ids.Any())
			{
				return BadRequest();
			}

			dataService.ApproveUsers(ids);

			return Ok();
		}

		[Route("single")]
		[HttpPut]
		public IHttpActionResult ApproveSingle(int? id)
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}

			return ApproveByIds(new[] { id.Value });
		}

		[HttpPost]
		public IHttpActionResult ApprovePending()
		{
			dataService.InitializeApprovedUsers();
			return base.Ok();
		}
	}
}
