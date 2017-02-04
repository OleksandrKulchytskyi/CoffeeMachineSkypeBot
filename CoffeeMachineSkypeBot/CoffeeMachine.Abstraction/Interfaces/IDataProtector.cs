namespace CoffeeMachine.Abstraction
{
	public interface IDataProtector
	{
		string Encrypt(string data, string phrase);
		string Decrypt(string data, string phrase);
	}
}
