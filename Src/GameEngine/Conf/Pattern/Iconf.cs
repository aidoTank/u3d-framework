namespace GameEngine
{
    public interface Iconf<T> where T: IReader
    {
        void OnLoad(T reader);
    }
}
