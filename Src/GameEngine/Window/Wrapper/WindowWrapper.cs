/***
 * WindowMediator.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class WindowWrapper<T> : WindowMediator
    {
        public static void Open()
        {
            WindowManager.Open<T>(null, null);
        }

        public static void Open(System.Object param)
        {
            WindowManager.Open<T>(null, param);
        }

        public static void SetActive(bool isActive)
        {
            WindowManager.SetActive<T>(null, isActive);
        }

        public static void Close()
        {
            WindowManager.Close<T>();
        }
    }
}
