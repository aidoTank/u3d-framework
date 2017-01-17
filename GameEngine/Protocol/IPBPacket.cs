/***
 * IPBPacket.cs
 * 
 * @author abaojin
 */
namespace GameEngine
{
    public interface IPBPacket
    {
        byte[] Encoder();

        void Decoder(byte[] bytes);
    }
}
