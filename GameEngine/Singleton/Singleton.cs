/***
 * Singleton.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class Singleton<T> where T : new()
    {
        private static T instance;

        protected Singleton()
        {
        }

        public static T Instance 
        {
            get {
                if (instance == null) {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}