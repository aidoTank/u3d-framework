using System;

/***
 * ActionHolder.cs
 * 
 * @author abaojin
 */
namespace GameEngine
{
    public class ActionHolder
    {
        public string Id;
        public Action<System.Object> Action;

        public ActionHolder(string id, Action<System.Object> action)
        {
            this.Id = id;
            this.Action = action;
        }
    }
}
