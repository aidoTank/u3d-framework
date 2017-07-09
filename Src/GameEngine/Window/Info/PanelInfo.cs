using UnityEngine;

/***
 * PanelInfo.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    [System.Serializable]
    public class PanelInfo
    {
        [SerializeField]
        private string m_key;

        private GameObject m_gOject;
        private object m_openParam;

        public string Key 
        {
            get {
                return m_key;
            }
            set {
                m_key = value;
            }
        }

        public GameObject GObject
        {
            get {
                return m_gOject;
            }
            set {
                m_gOject = value;
            }
        }

        public object OpenParam 
        {
            get {
                return m_openParam;
            }
            set {
                m_openParam = value;
            }
        }

        public bool ActiveSelf 
        {
            get {
                if(GObject == null) {
                    return false;
                }
                return GObject.activeSelf;
            }
        }

        public void SetActive(bool isActive)
        {
            if(GObject == null) {
                return;
            }

            m_gOject.SetActive(isActive);
        }
    }
}
