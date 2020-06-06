using UnityEngine;

/***
 * PanelBase.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    /// <summary>
    /// 所有界面基类
    /// </summary>
    public class PanelBase : MonoBehaviour
    {
        [SerializeField]
        private PanelInfo m_panelInfo;

        public PanelInfo PanelInfo 
        {
            get {
                return m_panelInfo;
            }
            private set {
                m_panelInfo = value;
            }
        }

        public virtual void Init()
        {

        }
    }
}

