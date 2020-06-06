using System;
using System.Collections.Generic;

/***
 * ConfPool.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public static class ConfPool
    {
        private static Dictionary<Type, Dictionary<string, IConfData>> m_tabPools;
        private static Dictionary<Type, IConfData> m_iniPool;
        private static Dictionary<Type, IConfData> m_jsnPool;

        static ConfPool()
        {
            m_tabPools = new Dictionary<Type, Dictionary<string, IConfData>>();

            m_iniPool = new Dictionary<Type, IConfData>();
            m_jsnPool = new Dictionary<Type, IConfData>();
        }

        #region Conf 初始化

        public static void InitTabConf<T1, T2>() where T2 : AbsTabConf, new()
        {
            Type type = typeof(T1);
            if (m_tabPools.ContainsKey(type)) {
                GameLog.Error(string.Format("Tab {0} 已加载", type.Name));
                return;
            }

            T2 cfg = new T2();
            cfg.Init();
            m_tabPools.Add(type, cfg.ConfPool);

            cfg = null;
        }

        public static void InitIniConf<T1, T2>() where T2 : AbsIniConf, new()
        {
            Type type = typeof(T1);
            if (m_iniPool.ContainsKey(type)) {
                GameLog.Error(string.Format("Ini {0} 已加载", type.Name));
                return;
            }

            T2 cfg = new T2();
            cfg.Init();
            m_iniPool.Add(type, cfg.ConfData);

            cfg = null;
        }

        public static void InitJsonConf<T1, T2>() where T2 : AbsJsonConf, new()
        {
            Type type = typeof(T1);
            if (m_jsnPool.ContainsKey(type)) {
                GameLog.Error(string.Format("Json {0} 已加载", type.Name));
                return;
            }

            T2 cfg = new T2();
            cfg.Init();
            m_jsnPool.Add(type, cfg.ConfData);

            cfg = null;
        }

        #endregion

        #region Tab 接口

        public static T GetTab<T>(string key) where T : IConfData
        {
            Type type = typeof(T);

            Dictionary<string, IConfData> tabPool = null;
            if (!m_tabPools.TryGetValue(type, out tabPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return default(T);
            }

            IConfData tab = null;
            if (!tabPool.TryGetValue(key, out tab)) {
                GameLog.Error(string.Format("{0} 表不存在Key {1}", type.Name, key));
                return default(T);
            }

            return (T)tab;
        }

        public static T GetTab<T>(int? key) where T : IConfData
        {
            if(key == null) {
                return default(T);
            }
            return GetTab<T>(key.ToString());
        }

        public static Dictionary<string, IConfData> GetTabAll<T>() where T : IConfData
        {
            Type type = typeof(T);

            Dictionary<string, IConfData> tabPool = null;
            if (!m_tabPools.TryGetValue(type, out tabPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return null;
            }

            return tabPool;
        }

        public static int GetTabCount<T>() where T : IConfData
        {
            Type type = typeof(T);

            Dictionary<string, IConfData> tabPool = null;
            if (!m_tabPools.TryGetValue(type, out tabPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return -1;
            }

            return tabPool.Count;
        }

        public static bool HasTab<T>() where T : IConfData
        {
            return m_tabPools.ContainsKey(typeof(T));
        }

        #endregion

        #region Ini 接口

        public static T GetIni<T>() where T : IConfData
        {
            Type type = typeof(T);

            IConfData ini = null;
            if (!m_iniPool.TryGetValue(type, out ini)) {
                GameLog.Error(string.Format("Ini {0} 不存在", type.Name));
                return default(T);
            }

            return (T)ini;
        }

        public static int GetIniCount()
        {
            return m_iniPool.Count;
        }

        public static bool RemoveIni<T>() where T : IConfData
        {
            bool result = true;

            Type type = typeof(T);

            IConfData ini = null;
            if (m_iniPool.TryGetValue(type, out ini)) {
                result = m_iniPool.Remove(type);
            }

            return result;
        }


        #endregion

        #region Json 接口

        public static T GetJson<T>() where T : IConfData
        {
            Type type = typeof(T);

            IConfData json = null;
            if (!m_jsnPool.TryGetValue(type, out json)) {
                GameLog.Error(string.Format("Json {0} 不存在", type.Name));
                return default(T);
            }

            return (T)json;
        }

        public static int GetJsonCount()
        {
            return m_jsnPool.Count;
        }

        public static bool RemoveJson<T>() where T : IConfData
        {
            bool result = true;

            Type type = typeof(T);

            IConfData json = null;
            if (m_jsnPool.TryGetValue(type, out json)) {
                result = m_iniPool.Remove(type);
            }

            return result;
        }

        #endregion

        public static void RemoveAll()
        {
            m_tabPools.Clear();
            m_tabPools = null;

            m_jsnPool.Clear();
            m_jsnPool = null;

            m_iniPool.Clear();
            m_iniPool = null;
        }
    }
}
