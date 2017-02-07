using System.Collections;
using UnityEngine;

/***
 * CoroutineUtils.cs
 * 
 * @author abaojin
 */
namespace GameEngine
{
    public class CoroutineUtils
    {
        private static MonoBehaviour globalCoroutine;

        public static MonoBehaviour GlobalCoroutine 
        {
            get {
                if (globalCoroutine == null) {
                    GameObject go = new GameObject("_globalCoroutine");
                    UnityEngine.Object.DontDestroyOnLoad(go);
                    globalCoroutine = go.AddComponent<CoroutineComponent>();
                }
                return globalCoroutine;
            }
        }

        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null) {
                return null;
            }
            return GlobalCoroutine.StartCoroutine(routine);
        }

        public static void StopCoroutine(IEnumerator routine)
        {
            GlobalCoroutine.StopCoroutine(routine);
        }

        public static void StopCoroutine(string methodName)
        {
            GlobalCoroutine.StopCoroutine(methodName);
        }
    }
}
