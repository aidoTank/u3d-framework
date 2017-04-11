using GameCode;
using GameEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class ApplicationStart : MonoBehaviour
    {
        void Start()
        {
            ConfInitPool.InitConf();
            //TextAsset asset = Resources.Load<TextAsset>("test.tab");
            //Debug.LogError(asset.text);

            SceneTab tab = ConfPool.GetTab<SceneTab>(1002.ToString());
            Debug.LogError(tab.Des);
        }

        void Update()
        {
            Image image;
        }
    }
}
