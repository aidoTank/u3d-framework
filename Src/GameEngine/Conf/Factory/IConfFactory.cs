namespace GameEngine
{
    public interface IConfFactory
    {
        bool Load<T>(string file, Iconf<T> conf, bool isInternal) where T : IReader, new();
    }
}
