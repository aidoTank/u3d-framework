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
        private static List<string> m_viewList = new List<string>();
        private static Facade m_facade = Facade.Instance;

        private static GameObject m_root;

        private static Queue<string> m_openQueue = new Queue<string>();

        private static GameObject Root
        {
            get {
                if (m_root == null) {
                    m_root = GameObject.Find("UI Root");
                }
                return m_root;
            }
        }

        public static GameObject GetObject(string viewName)
        {
            if (Root == null) {
                return null;
            }

            Transform tr = Root.transform.FindChild(viewName);
            if (tr == null) {
                return null;
            }

            return tr.gameObject;
        }

        public static void AddWindow<T>() where T : WindowMediatorBase
        {
            string name = typeof(T).FullName;

            if (!m_viewList.Contains(name)) {
                m_viewList.Add(name);
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

            WindowMediatorBase baseMediator = meditor as WindowMediatorBase;
            baseMediator.DoOpen(key, param);
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
            int count = m_viewList.Count;
            if (count == 0) {
                return;
            }

            for (int i = 0; i < count; ++i) {
                string name = m_viewList[i];
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
            int count = m_viewList.Count;
            if (count == 0) {
                return;
            }

            for (int i = 0; i < count; ++i) {
                string name = m_viewList[i];
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
                ViewInfo vo = window.ViewVO;
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
            int count = m_viewList.Count;
            if (count == 0) {
                return;
            }
            for (int i = 0; i < count; ++i) {
                string name = m_viewList[i];
                m_facade.RemoveMediator(name);
            }

            m_viewList.Clear();
            m_openQueue.Clear();

            m_root = null;
        }
    }
}

