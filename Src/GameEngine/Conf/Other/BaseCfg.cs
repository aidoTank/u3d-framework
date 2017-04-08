using System.Collections.Generic;

/***
 * BaseConfig.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public abstract class BaseCfg
    {
        private Dictionary<string, BaseTbl> m_tbl;

        public Dictionary<string, BaseTbl> Tbl
        {
            get {
                return m_tbl;
            }
            set {
                m_tbl = value;
            }
        }

        public abstract void Init();

        public virtual TableSheet Parse(string context)
        {
            TableSheet sheet = null;
            if (string.IsNullOrEmpty(context)) {
                GameLog.Error("表内容为空");
                return null;
            }

            sheet = new TableSheet();
            string[] rowArray = context.Split('\n');
            int length = rowArray.Length;
            for (int i = 0; i < length; i++) {
                string line = rowArray[i];
                if (line.StartsWith("#")) {
                    continue;
                }
                string[] fields = line.Split('\t');
                if (fields.Length > 1) {
                    sheet.ReadLine(fields);
                }
            }
            return sheet;
        }
    }
}
