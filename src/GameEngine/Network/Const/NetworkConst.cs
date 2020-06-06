/***
 * NetworkConst.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class NetworkConst
    {
        /*
         * 网络重连次数 
         */
        public const byte RECONNECT_COUNT = 3;

        /*
         * 每帧处理包最大数
         */ 
        public const byte FRAME_MAX_PROCESS = 10;
    }
}
