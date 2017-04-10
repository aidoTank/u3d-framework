namespace GameEngine
{
    public interface IConfFactory
    {
        bool Load<T>(string file, Iconf<T> conf, bool isInternal) where T : IReader, new();

        bool UnLoad<T>(Iconf<T> conf, bool isUnload = true) where T : IReader;
    }
}
