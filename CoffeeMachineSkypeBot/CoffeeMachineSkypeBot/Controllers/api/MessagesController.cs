using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Models;

namespace CoffeeMachineSkypeBot
{
	[BotAuthentication]
	public class MessagesController : ApiController
	{
		private readonly ICommandHandler commandHandler;
		private readonly IDataService dataService;

		public MessagesController(ICommandHandler optionHandler,
								  IDataService dataService)
		{
			this.commandHandler = optionHandler;
			this.dataService = dataService;
		}

		/// <summary>
		/// POST: api/Messages
		/// Receive a message from a user and reply to it
		/// </summary>
		public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
		{
			if (activity.Type == ActivityTypes.Message)
			{
				var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
				var uid = activity.From == null ? String.Empty : activity.From.Id;

				UserStatus status = dataService.CheckUserStatus(uid);
				if (status == UserStatus.PendindApproval)
				{
					var msg = $"Sorry your user [{activity.From.Name}] wasn't approved by the system administrator.";
					return await SendMessageToActivityAsync(activity, connector, msg);
				}
				else if (status == UserStatus.Inactive)
				{
					var msg = $"Sorry your user  [{activity.From.Name}] is disabled in the system.";
					return await SendMessageToActivityAsync(activity, connector, msg);
				}
				var loweredCommand = String.IsNullOrEmpty(activity.Text) ? String.Empty : activity.Text.ToLower();
				if (commandHandler.CanHandle(loweredCommand) &&
					status == UserStatus.Active)
				{
					var result = commandHandler.HandleCommand(loweredCommand, uid);
					await SendMessageToActivityAsync(activity, connector, result);
					return Request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					// return our reply to the user
					await SendMessageToActivityAsync(activity, connector, $"You sent to CoffeeMachine bot unsupported command: {activity.Text}");
				}
			}
			else
			{
				HandleSystemMessage(activity);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		private async Task<HttpResponseMessage> SendMessageToActivityAsync(Activity activity, ConnectorClient connector, string msg)
		{
			await connector.Conversations.ReplyToActivityAsync(activity.CreateReply(msg, "en"));
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		private Activity HandleSystemMessage(Activity activity)
		{
			if (activity.Type == ActivityTypes.DeleteUserData)
			{
				// Implement user deletion here
				// If we handle user deletion, return a real message
			}
			else if (activity.Type == ActivityTypes.ConversationUpdate)
			{
				// Handle conversation state changes, like members being added and removed
				// Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
				// Not available in all channels
			}
			else if (activity.Type == ActivityTypes.ContactRelationUpdate)
			{
				// Handle add/remove from contact lists
				// Activity.From + Activity.Action represent what happened
				HandleMembers(activity);
			}
			else if (activity.Type == ActivityTypes.Typing)
			{
				// Handle knowing that the user is typing
			}
			else if (activity.Type == ActivityTypes.Ping)
			{
			}

			return null;
		}

		private void HandleMembers(Activity activity)
		{
			if (activity.Action == ContactRelationUpdateActionTypes.Add)
			{
				if (activity.MembersAdded != null &&
					activity.MembersAdded.Any())
				{
					var users = activity.MembersAdded.Select(x => new AddUserRequest
					{
						UserId = x.Id,
						UserName = x.Name
					})
													.ToArray();
					dataService.AddUserForApprovalQueue(users);
				}

				if (activity.From != null &&
					!String.IsNullOrEmpty(activity.From.Name))
				{
					var newUser = new AddUserRequest
					{
						UserId = activity.From.Id,
						UserName = activity.From.Name
					};
					dataService.AddUserForApprovalQueue(new[] { newUser });
				}
			}
		}
	}
}