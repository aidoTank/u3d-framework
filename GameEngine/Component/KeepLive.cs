using System.Collections.Generic;
using UnityEngine;

/***
 * KeepLive.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class KeepLive : MonoBehaviour
    {
        private static List<int> ListObject = new List<int>();
        public int Id = 0;

        private void Awake()
        {
            if (ListObject.Contains(Id)) {
                Destroy(gameObject);
                return;
            }
            ListObject.Add(Id);
            DontDestroyOnLoad(gameObject);
        }
    }
}

