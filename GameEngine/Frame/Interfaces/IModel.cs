/**
 * IModel.cs
 * 
 * author : abaojin 
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
