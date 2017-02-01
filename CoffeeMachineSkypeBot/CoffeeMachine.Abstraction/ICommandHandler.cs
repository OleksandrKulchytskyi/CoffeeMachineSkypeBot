
namespace CoffeeMachine.Abstraction
{
    public interface ICommandHandler
	{
		bool CanHandle(string command);

		string HandleCommand(string command, string uid);
	}
}
