using CoffeeMachine.Abstraction.Interfaces;
using CoffeeMachine.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace UnitTestProject1
{
	[TestClass]
	public class UserActivityImportTests
	{
		[TestMethod]
		public void ShouldPassWithNoErrors()
		{
			IUserActivityImporter importer = new UserActivityImporter();
			string data = @"2017/01/30 16:20:38, 228-13-239-26, Unknown
							2016/02/20 12:20:38, 228-13-239-26, Unknown2
							2017/02/18 14:30:15, 29:19fPMDMoT4Ce9UXQd9Pcx - YtqFkoG0PkHE6r7DfvvQPg
							2017/03/26 09:49:35, 36 - 246 - 239 - 26, 29:19fPMDMoT4Ce9UXQd9Pcx-YtqG0PkHE6r7DfvvQPg
							2017/04/03 16:30:23, 29:19fPMDMoT4Ce9UXQd9Pcx - YtqFkoG0PkHE6r7DQPg
							2017/04/03 16:30:23, ""29:19fPMDMoT4Ce9UXQd9Pcx - YtqFkoG0PkHE6r7DQPg""";

			byte[] array = Encoding.UTF8.GetBytes(data);

			var result = importer.ImportFrom(new MemoryStream(array));

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Count, 6);
			Assert.AreEqual(importer.Errors.Count, 0);
			Assert.IsTrue(result.TrueForAll(r => !String.IsNullOrEmpty(r.UserIdentifier)));
		}

		[TestMethod]
		public void ShouldPassWithSomeErrors()
		{
			IUserActivityImporter importer = new UserActivityImporter();
			string data = @"2017/040 16:20:38, 228-13-239-26, Unknown
							2016/02/20 12:20:38 Unknown2";

			byte[] array = Encoding.UTF8.GetBytes(data);

			var result = importer.ImportFrom(new MemoryStream(array));

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Count, 0);
			Assert.AreEqual(importer.Errors.Count, 2);
		}
	}
}
