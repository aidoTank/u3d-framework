/***
 * IPBPacket.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public interface IPBPacket
    {
        byte[] Encoder();

        void Decoder(byte[] bytes);
    }
}
