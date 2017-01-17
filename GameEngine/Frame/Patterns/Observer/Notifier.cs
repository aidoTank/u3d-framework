/**
 * Notifier.cs
 * 
 * author : abaojin 
 * 
 */
namespace GameEngine
{
    public class Notifier : INotifier
    {
        private IFacade mFacade = GameEngine.Facade.Instance;

        public IFacade Facade 
        {
            get { return mFacade; }
        }

        public virtual void SendNotification(string notificationName) 
		{
			mFacade.SendNotification(notificationName);
		}

		public virtual void SendNotification(string notificationName, object body)
		{
			mFacade.SendNotification(notificationName, body);
		}
		public virtual void SendNotification(string notificationName, object body, string type)
		{
            mFacade.SendNotification(notificationName, body, type);
		}

	}
}
