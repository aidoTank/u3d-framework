using UnityEngine;

/***
 * SingletonComponent.cs
 * 
 * @author abaojin
 */
namespace GameEngine
{
    public class SingletonComponent<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get {
                if (instance == null) {
                    GameObject go = new GameObject("_Singleton" + typeof(T).Name);
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<T>();
                }

                return instance;
            }
        }

    }
}
