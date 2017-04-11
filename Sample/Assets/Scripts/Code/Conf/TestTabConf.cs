using GameEngine;

namespace GameCode
{
    public class TestTabConf : AbsTabConf
    {
        public const string FILE_NAME = "test.tab";

        public enum Cols
        {
            KEY,
            DES,
            AGE
        }

        public override void Init()
        {
            ConfFactory.LoadConf<TabReaderImpl>(FILE_NAME, this);
        }

        public override void OnRow(ITabRow row)
        {
            TestDataTab tab = new TestDataTab();
            tab.Key = row.GetString((int)Cols.KEY);
            tab.Des = row.GetString((int)Cols.DES);
            tab.Age = row.GetInt((int)Cols.AGE);

            if (!ConfPool.ContainsKey(tab.Key)) {
                ConfPool.Add(tab.Key, tab);
            }
        }
    }

    public class TestDataTab : IConfData
    {
        public string Key;
        public string Des;
        public int Age;
    }
}
