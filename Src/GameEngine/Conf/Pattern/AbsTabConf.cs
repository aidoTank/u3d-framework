using System.Collections.Generic;

/***
 * AbsTabConf.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public abstract class AbsTabConf : Iconf<TabReaderImpl>
    {
        private Dictionary<string, IConfData> confPool;

        public AbsTabConf()
        {
            this.confPool = new Dictionary<string, IConfData>();
        }

        public Dictionary<string, IConfData> ConfPool
        {
            get {
                 return confPool;
            }
        }

        public void OnLoad(TabReaderImpl reader)
        {
            if(reader == null) {
                return;
            }
            reader.ParseRow((ITabRow row) => {
                this.OnRow(row);
            });
        }

        public abstract void Init();

        public abstract void OnRow(ITabRow row);
    }
}
