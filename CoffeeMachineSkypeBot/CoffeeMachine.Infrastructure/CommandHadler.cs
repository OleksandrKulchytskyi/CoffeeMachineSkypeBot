using System;
using CoffeeMachine.Abstraction;
using System.Collections.Generic;

namespace CoffeeMachine.Infrastructure
{
	public class CommandHadler : ICommandHandler
	{
		private const string NoCountMessage = "System is unable to retrieve information";
		private readonly IDataRetrieval dataService;
		private readonly Dictionary<string, Func<string, string>> comandHandlers;

		public CommandHadler(IDataRetrieval dalService)
		{
			dataService = dalService;

			comandHandlers = new Dictionary<string, Func<string, string>>(3);
			comandHandlers.Add("day", HandleDayCommand);
			comandHandlers.Add("month", HandleMonthCommand);
			comandHandlers.Add("year", HandleYearCommand);
		}
		#region Private

		private string HandleDayCommand(string uid)
		{
			return DoHandleCommon(uid, AggregationType.Day);
		}

		private string HandleMonthCommand(string uid)
		{
			return DoHandleCommon(uid, AggregationType.Month);
		}

		private string HandleYearCommand(string uid)
		{
			return DoHandleCommon(uid, AggregationType.Year);
		}

		private string DoHandleCommon(string uid, AggregationType type)
		{
			var count = dataService.Aggregate(uid, type);
			if (count.HasValue)
			{
				return $"User - {uid} has made - {count} cup(s)";
			}
			return NoCountMessage;
		} 
		#endregion

		public bool CanHandle(string command)
		{
			return comandHandlers.ContainsKey(command);
		}

		public string HandleCommand(string command, string uid)
		{
			command = command.ToLower().Trim();

			if (CanHandle(command))
			{
				return comandHandlers[command](uid);
			}

			return String.Empty;
		}
	}
}
