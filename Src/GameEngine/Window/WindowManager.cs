using System.Collections.Generic;
using UnityEngine;

/***
 * WindowManager.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public static class WindowManager
    {
        // 显示界面容器
        private static List<string> m_panelKeyList = new List<string>();
        private static Dictionary<string, GameObject> m_panelObjList = new Dictionary<string, GameObject>();
        private static Facade m_facade = Facade.Instance;

        private static GameObject m_root;

        private static Queue<string> m_openQueue = new Queue<string>();

        private static GameObject Root
        {
            get {
                if (m_root == null) {
                    m_root = GameObject.Find("Window/Panel");
                }
                return m_root;
            }
        }

        public static GameObject GetObject(string key)
        {
            if (Root == null) {
                throw new FrameworkException("Window Panel is null.");
            }

            if (m_panelObjList.ContainsKey(key)) {
                return m_panelObjList[key];
            }

            GameObject obj = Resources.Load<GameObject>(key);
            if (obj == null) {
                Debug.LogError("Panel is null.");
                return null;
            }

            GameObject instanceObj = GameObject.Instantiate(obj);
            instanceObj.transform.parent = Root.transform;
            m_panelObjList.Add(key, instanceObj);

            return instanceObj;
        }

        public static void AddWindow<T>() where T : WindowMediatorBase
        {
            string name = typeof(T).FullName;

            if (!m_panelKeyList.Contains(name)) {
                m_panelKeyList.Add(name);
            } else {
                Debug.Log(string.Format("{0} mediator is already add.", name));
            }
        }

        public static T GetWindow<T>() where T : WindowMediatorBase
        {
            string name = typeof(T).FullName;

            IMediator mediator = m_facade.GetMediator(name);
            if(mediator != null) {
                return (T)mediator;
            }

            return default(T);
        }

        public static void Open<T>(string key, object param)
        {
            string name = typeof(T).FullName;

            IMediator meditor = m_facade.GetMediator(name);
            if(meditor == null) {
                return;
            }

            if(meditor is WindowMediatorBase) {
                (meditor as WindowMediatorBase).DoOpen(key, param);
            } else {
                throw new FrameworkException("Not exist window mediator.");
            }
        }

        public static void SetActive<T>(string key, bool isActive)
        {
            string name = typeof(T).FullName;

            IMediator meditor = m_facade.GetMediator(name);
            if (meditor == null) {
                return;
            }

            WindowMediatorBase baseMediator = meditor as WindowMediatorBase;
            baseMediator.DoActive(key, isActive);
        }

        public static void SetAllActive(bool isActive)
        {
            int count = m_panelKeyList.Count;
            if (count == 0) {
                return;
            }

            for (int i = 0; i < count; ++i) {
                string name = m_panelKeyList[i];
                IMediator meditor = m_facade.GetMediator(name);
                if (meditor == null) {
                    continue;
                }

                if(meditor is WindowMediatorBase) {
                    (meditor as WindowMediatorBase).DoActive(null, isActive);
                } else {
                    throw new FrameworkException("Not exist window mediator.");
                }
            }
        }

        public static void Close<T>()
        {
            string name = typeof(T).FullName;

            IMediator meditor = m_facade.GetMediator(name);
            if (meditor == null) {
                return;
            }

            if(meditor is WindowMediatorBase) {
                (meditor as WindowMediatorBase).DoClose();
            } else {
                throw new FrameworkException("Not exist window mediator.");
            }
        }

        public static void CloseAll()
        {
            int count = m_panelKeyList.Count;
            if (count == 0) {
                return;
            }

            for (int i = 0; i < count; ++i) {
                string name = m_panelKeyList[i];
                IMediator meditor = m_facade.GetMediator(name);
                if(meditor == null) {
                    continue;
                }

                if(meditor is WindowMediatorBase) {
                    (meditor as WindowMediatorBase).DoClose();
                } else {
                    throw new FrameworkException("Not exist window mediator.");
                }
            }

            m_openQueue.Clear();
        }

        public static bool IsOpen(string name)
        {
            IMediator meditor = m_facade.GetMediator(name);
            if (meditor == null) {
                return false;
            }

            bool objActive = false;
            if(meditor is WindowMediator) {
                WindowMediator window = meditor as WindowMediator;
                PanelInfo vo = window.PanelInfo;
                if (vo != null) {
                    objActive = vo.ActiveSelf;
                }
            } else {
                Debug.LogError("Not exist panel mediator.");
                return false;
            }

            return objActive;
        }

        public static void Destroy<T>()
        {
            m_facade.RemoveMediator(typeof(T).FullName);
        }

        public static void DestroyAll()
        {
            int count = m_panelKeyList.Count;
            if (count == 0) {
                return;
            }
            for (int i = 0; i < count; ++i) {
                string name = m_panelKeyList[i];
                m_facade.RemoveMediator(name);
            }

            m_panelKeyList.Clear();
            m_openQueue.Clear();

            m_root = null;
        }
    }
}

