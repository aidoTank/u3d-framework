using UnityEditor;
using UnityEngine;

/***
 * APIHelperEditor.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    public class APIHelperEditor
    {
        [MenuItem("Assets/Helper/FindGameObjects")]
        public static void GetAllSceneObject()
        {
            // GameObject[] objs = (GameObject[])GameObject.FindObjectsOfTypeAll(typeof(GameObject));// old api
            // 注意这个apif返回大于场景显示对象
            GameObject[] objs = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
            Debug.Log(string.Format("Current GameObject Count Is: {0}", objs.Length));
            foreach(GameObject obj in objs) {
                Debug.Log(obj.name);
            }
        }
    }
}
