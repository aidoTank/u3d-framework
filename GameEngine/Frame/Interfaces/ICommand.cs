/**
 * ICommand.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameEngine
{
    public interface ICommand
    {
		void Execute(INotification notification);
    }
}
