using System;

namespace CoffeeMachine.Abstraction.Dto
{
	public abstract class ExportImportBase
	{
		public int RowId { get; set; }
	}

	public class ImportDataContainer : ExportImportBase
	{
		public DateTime Date { get; set; }

		public string UserIdentifier { get; set; }

		public string SkypeId { get; set; }

		public override int GetHashCode()
		{
			return String.IsNullOrEmpty(UserIdentifier) ?
					base.GetHashCode() : UserIdentifier.GetHashCode();
		}

		public override string ToString()
		{
			return String.Format("{0} - {1}", Date.ToString("dd-MM-yyyy, HH:mm:ss"), UserIdentifier);
		}
	}

	public class ImportValidationResult : ExportImportBase
	{
		public string Message { get; set; }
	}
}
