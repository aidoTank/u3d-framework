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
            StaticBatchingUtility.Combine(gameObject);
        }

        void OnEnable()
        {
            Destroy(this);
        }
    }
}
