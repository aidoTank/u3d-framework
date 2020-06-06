﻿/**
 * INotifier.cs
 * 
 * author : administrator 
 * 
 */
namespace GameEngine
{
    public interface INotifier
    {
		void SendNotification(string notificationName);

		void SendNotification(string notificationName, object body);

		void SendNotification(string notificationName, object body, string type);
    }
}
