/**
 * IProxy.cs
 * 
 * author : administrator 
 * 
 */
namespace GameEngine
{
    public interface IProxy
    {
		string ProxyName { get; }

		object Data { get; set; }

		void OnRegister();

		void OnRemove();
    }
}
