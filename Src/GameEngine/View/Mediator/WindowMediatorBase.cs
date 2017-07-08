using UnityEngine;

/***
 * ViewSingleMediator.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class WindowMediator : WindowMediatorBase
    {
        private ViewInfo mViewInfo;

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
            ViewInfo info = new ViewInfo();

            info.Key = name;
            info.GObject = WindowManager.GetObject(name);
            info.OpenParam = null;
            info.SetActive(false);

            mViewInfo = info;
        }

        public override void DoActive(string key, bool isActive)
        {
            if(ViewVO == null) {
                return;
            }

            ViewVO.SetActive(isActive);
        }

        public override void DoOpen(string key, System.Object param)
        {
            if (ViewVO == null) {
                return;
            }

            if(ViewVO.GObject == null) {
                ViewVO.GObject = WindowManager.GetObject(ViewVO.Key);
            }

            ViewVO.SetActive(true);
            ViewVO.OpenParam = param;

            OnOpen(param);
        }

        public override void DoClose()
        {
            if (ViewVO == null) {
                return;
            }

            ViewVO.SetActive(false);
            ViewVO.OpenParam = null;

            OnClose();
        }

        public ViewInfo ViewVO 
        {
            get {
                return mViewInfo;
            }
        }

        public string Key 
        {
            get {
                if (ViewVO == null) {
                    return null;
                }
                return ViewVO.Key;
            }
        }

        public GameObject GObject 
        {
            get {
                if(ViewVO == null) {
                    return null;
                }
                return ViewVO.GObject;
            }
        }

        public object Param 
        {
            get {
                if (ViewVO == null) {
                    return null;
                }
                return ViewVO.OpenParam;
            }
        }
       
    }
}
