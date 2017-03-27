using UnityEngine;

/***
 * BatchingRenderer.cs 
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class BatchingRenderer : MonoBehaviour
    {
        void Awake()
        {
            Combine(gameObject);
        }

        void OnEnable()
        {
            Destroy(this);
        }

        public static void Combine(GameObject obj)
        {
            StaticBatchingUtility.Combine(obj);
        }
    }
}
