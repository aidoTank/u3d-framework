namespace GameEngine
{
    public interface IReader
    {
        bool Open(string name, bool isInternal);

        void Close();
    }
}
