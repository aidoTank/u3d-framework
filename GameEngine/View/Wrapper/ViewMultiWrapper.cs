/***
 * ViewMultiWrapper.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class ViewMultiWrapper<T> : ViewMultiMediator
    {
        public static void Open(string key)
        {
            ViewManager.Open<T>(key, null);
        }

        public static void Open(string key, System.Object param)
        {
            ViewManager.Open<T>(key, param);
        }

        public static void SetActive(string key, bool isActive)
        {
            ViewManager.SetActive<T>(key, isActive);
        }

        public static void Close()
        {
            ViewManager.Close<T>();
        }
    }
}
