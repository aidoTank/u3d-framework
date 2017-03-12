/**
 * ICommand.cs
 * 
 * author : administrator 
 * 
 */
namespace GameEngine
{
    public interface ICommand
    {
		void Execute(INotification notification);
    }
}
