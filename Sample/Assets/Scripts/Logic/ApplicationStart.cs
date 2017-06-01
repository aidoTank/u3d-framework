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

            SceneTab tab = ConfPool.GetTab<SceneTab>(1002);
            Debug.LogError(tab.Des);
        }

        void Update()
        {
            Image image;
        }
    }
}
