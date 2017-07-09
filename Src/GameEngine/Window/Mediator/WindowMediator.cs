/***
 * WindowMediatorBase.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public abstract class WindowMediatorBase : Mediator
    {
        /// <summary>
        /// 实例化时事件
        /// </summary>
        public virtual void OnNew()
        {

        }

        /// <summary>
        /// 界面打开事件
        /// </summary>
        /// <param name="param"></param>
        public virtual void OnOpen(object param)
        {

        }

        /// <summary>
        /// 界面关闭事件
        /// </summary>
        public virtual void OnClose()
        {

        }

        /// <summary>
        /// 界面销毁事件
        /// </summary>
        public virtual void OnDestroy()
        {

        }

        /// <summary>
        /// 添加面板
        /// </summary>
        /// <param name="name"></param>
        protected abstract void AddPanel(string name);

        /// <summary>
        /// 获取面板path
        /// </summary>
        /// <returns></returns>
        public virtual string GetPath(string name)
        {
            if(name == null) {
                return string.Empty;
            }

            string pName = name.Substring(0, name.Length - 6);
            return string.Format("Window/{0}/{1}Panel", pName, pName);
        }

        /// <summary>
        /// 打开面板
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        public abstract void DoOpen(string key, object param);

        /// <summary>
        /// 激活面板
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isActive"></param>
        public abstract void DoActive(string name, bool isActive);

        /// <summary>
        /// 关闭面板
        /// </summary>
        public abstract void DoClose();
    }
}
