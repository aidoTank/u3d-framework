/***
 * ConfPool.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public interface IReader
    {
        bool Open(string name, bool isInternal);

        void Close();
    }
}
