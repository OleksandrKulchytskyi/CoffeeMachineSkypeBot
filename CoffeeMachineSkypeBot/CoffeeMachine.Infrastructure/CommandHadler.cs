using System;
using System.Linq;
using CoffeeMachine.Abstraction;

namespace CoffeeMachine.Infrastructure
{
	public class CommandHadler : ICommandHandler
	{
		private string[] commands = new string[] { "day", "month", "year" };

		public bool CanHandle(string command)
		{
			if (String.IsNullOrEmpty(command))
			{
				return false;
			}

			return command.Any(x => x.Equals(command.ToLower().Trim()));
		}

		public string HandleCommand(string command, string uid)
		{
			return command;
		}
	}
}
