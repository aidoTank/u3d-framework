using api;

/***
 * IBaseNetwork.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    interface IBaseNetwork
    {
        void SendMessage(PBBody msg, string key);

        void SendMessage(PBBody msg);

        void Disconnect();

        void Update();

        void Reconnect();

        void ProcessSendPacket();

        void ProcessBackPacket();
    }
}
