/***
 * SimpleCommand.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        public static string NAME = "SimpleCommand";

        public virtual void Execute(INotification notification)
		{
		}
	}
}
