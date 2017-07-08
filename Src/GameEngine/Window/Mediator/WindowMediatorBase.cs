using UnityEngine;

/***
 * WindowMediator.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class WindowMediator : WindowMediatorBase
    {
        private PanelInfo m_panelInfo;

        protected WindowMediator()
        {
            mMediatorName = GetType().FullName;
        }

        public override void OnRegister()
        {
            OnInit();
            OnStart();
        }

        public override void OnRemove()
        {
            
        }

        public override void AddPanel(string name)
        {
            PanelInfo info = new PanelInfo();

            info.Key = name;
            info.GObject = null;
            info.OpenParam = null;
            info.SetActive(false);

            m_panelInfo = info;
        }

        public override void DoActive(string key, bool isActive)
        {
            if(PanelInfo == null) {
                return;
            }

            PanelInfo.SetActive(isActive);
        }

        public override void DoOpen(string key, object param)
        {
            if (PanelInfo == null) {
                return;
            }

            if(PanelInfo.GObject == null) {
                PanelInfo.GObject = WindowManager.GetObject(PanelInfo.Key);
            }

            PanelInfo.SetActive(true);
            PanelInfo.OpenParam = param;

            OnOpen(param);
        }

        public override void DoClose()
        {
            if (PanelInfo == null) {
                return;
            }

            PanelInfo.SetActive(false);
            PanelInfo.OpenParam = null;

            OnClose();
        }

        public PanelInfo PanelInfo 
        {
            get {
                return m_panelInfo;
            }
        }

        public string Key 
        {
            get {
                if (PanelInfo == null) {
                    return null;
                }
                return PanelInfo.Key;
            }
        }

        public GameObject GObject 
        {
            get {
                if(PanelInfo == null) {
                    return null;
                }
                return PanelInfo.GObject;
            }
        }

        public object Param 
        {
            get {
                if (PanelInfo == null) {
                    return null;
                }
                return PanelInfo.OpenParam;
            }
        }
       
    }
}
