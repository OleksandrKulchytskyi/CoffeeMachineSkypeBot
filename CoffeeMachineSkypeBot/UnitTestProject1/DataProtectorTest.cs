using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Infrastructure;

namespace UnitTestProject1
{
	[TestClass]
	public class DataProtectorTest
	{
		private IDataProtector protector;

		[TestInitialize]
		public void Setup()
		{
			protector = new StringDataProtector();
		}

		[TestMethod]
		public void DecryptEncryptShouldPass()
		{
			//Arrange
			string phrase = "testPhrase";
			var data = "I want to encrypt some data";

			//Act
			var encrypted = protector.Encrypt(data, phrase);
			var decrypted = protector.Decrypt(encrypted, phrase);

			//Assert
			StringAssert.Equals(decrypted, data);
		}
	}
}
