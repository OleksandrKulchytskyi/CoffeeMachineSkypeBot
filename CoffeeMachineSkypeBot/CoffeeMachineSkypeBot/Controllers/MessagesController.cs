using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using CoffeeMachine.Abstraction;

namespace CoffeeMachineSkypeBot
{
	[BotAuthentication]
	public class MessagesController : ApiController
	{
		private readonly ICommandHandler commandHandler;

		public MessagesController(ICommandHandler optionHandler)
		{
			this.commandHandler = optionHandler;
		}

		/// <summary>
		/// POST: api/Messages
		/// Receive a message from a user and reply to it
		/// </summary>
		public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
		{
			if (activity.Type == ActivityTypes.Message)
			{
				ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

				if (commandHandler.CanHandle(activity.Text))
				{
					var username = activity.From.Name;
					var result = commandHandler.HandleCommand(activity.Text, username);
					Activity commandReply = activity.CreateReply(result);
					await connector.Conversations.ReplyToActivityAsync(commandReply);
				}

				// return our reply to the user
				Activity reply = activity.CreateReply($"You sent to CoffeeMachine next message: {activity.Text}");
				await connector.Conversations.ReplyToActivityAsync(reply);
			}
			else
			{
				HandleMembers(activity);
				HandleSystemMessage(activity);
			}

			var response = Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}

		private void HandleMembers(Activity activity)
		{
			if (activity.MembersAdded.Any())
			{
			}

			if (activity.MembersRemoved.Any())
			{
			}
		}

		private Activity HandleSystemMessage(Activity message)
		{
			if (message.Type == ActivityTypes.DeleteUserData)
			{
				// Implement user deletion here
				// If we handle user deletion, return a real message
			}
			else if (message.Type == ActivityTypes.ConversationUpdate)
			{
				// Handle conversation state changes, like members being added and removed
				// Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
				// Not available in all channels
			}
			else if (message.Type == ActivityTypes.ContactRelationUpdate)
			{
				// Handle add/remove from contact lists
				// Activity.From + Activity.Action represent what happened
			}
			else if (message.Type == ActivityTypes.Typing)
			{
				// Handle knowing tha the user is typing
			}
			else if (message.Type == ActivityTypes.Ping)
			{
			}

			return null;
		}
	}
}