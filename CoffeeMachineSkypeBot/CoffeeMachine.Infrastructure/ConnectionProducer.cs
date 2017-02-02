using CoffeeMachine.Abstraction;

namespace CoffeeMachine.Infrastructure
{
	public class ConnectionProducer : IConnection
	{
		private readonly IDataProtector protector;
		private readonly string decryptedValue;

		public ConnectionProducer(IDataProtector protector, string decrypted)
		{
			this.protector = protector;
			this.decryptedValue = decrypted;
		}

		public string ConnectionText
		{
			get
			{
				return protector.Decrypt(decryptedValue, "coffeemachine");
			}
		}
	}
}
