using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CoffeeMachine.Abstraction.Dto;
using CoffeeMachine.Abstraction.Interfaces;

namespace CoffeeMachine.Infrastructure
{
	public class UserActivityImporter : IUserActivityImporter
	{
		private readonly CultureInfo provider = CultureInfo.InvariantCulture;
		private readonly string DateFormat = "yyyy/MM/dd HH:mm:ss";

		public UserActivityImporter()
		{
			Errors = new List<ImportValidationResult>(10);
		}

		public List<ImportValidationResult> Errors { get; private set; }

		public List<ImportDataContainer> ImportFrom(Stream stream)
		{
			var importResult = new List<ImportDataContainer>(50);
			if (Errors.Count > 0)
			{
				Errors = new List<ImportValidationResult>(10);
			}

			using (stream)
			using (StreamReader sr = new StreamReader(stream))
			{
				string line = null;
				int rowIndex = 0;
				while ((line = sr.ReadLine()) != null)
				{
					rowIndex++;
					var importData = ParseLine(line.Trim(), rowIndex);
					if (importData != null)
					{
						importData.RowId = rowIndex;
						importResult.Add(importData);
					}
				}
			}

			return importResult;
		}

		private ImportDataContainer ParseLine(string line, int rowNumber)
		{
			if (String.IsNullOrEmpty(line))
			{
				return null;
			}

			var splitted = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (splitted.Length < 2)
			{
				Errors.Add(new ImportValidationResult { RowId = rowNumber, Message = "Unsupported line format" });
				return null;
			}

			DateTime parsedDate;
			if (!DateTime.TryParseExact(splitted[0].TrimEnd(), DateFormat, provider, DateTimeStyles.None, out parsedDate))
			{
				Errors.Add(new ImportValidationResult { RowId = rowNumber, Message = $"Unsupported date/time format, should be: {DateFormat}" });
				return null;
			}

			if (splitted.Length == 2)
			{
				return new ImportDataContainer
				{
					Date = parsedDate,
					UserIdentifier = splitted[1].Trim().Replace("\"", String.Empty)
				};
			}
			else if (splitted.Length == 3)
			{
				return new ImportDataContainer
				{
					Date = parsedDate,
					SkypeId = splitted[1].Trim().Replace("\"", String.Empty),
					UserIdentifier = splitted[2].Trim().Replace("\"", String.Empty)
				};
			}

			return null;
		}
	}
}
