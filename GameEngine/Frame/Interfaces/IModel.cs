/**
 * IModel.cs
 * 
 * author : administrator 
 * 
 */
namespace GameEngine
{
    public interface IModel
    {
		void RegisterProxy(IProxy proxy);

		IProxy GetProxy(string proxyName);

        IProxy RemoveProxy(string proxyName);

		bool HasProxy(string proxyName);
    }
}
