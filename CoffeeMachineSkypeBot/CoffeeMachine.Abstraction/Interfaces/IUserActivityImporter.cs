using System;
using System.Collections.Generic;
using System.IO;

namespace CoffeeMachine.Abstraction.Interfaces
{
	public interface  IUserActivityImporter
	{
		List<Dto.ImportDataContainer> ImportFrom(Stream stream);

		List<Dto.ImportValidationResult> Errors { get; }
	}
}
