/***
 * INotification.cs
 * 
 * @author : administrator 
 */
namespace GameEngine
{
    public interface INotification
    {
		string Name { get; }

		object Body { get; set; }

		string Type { get; set; }

        string ToString();
    }
}
