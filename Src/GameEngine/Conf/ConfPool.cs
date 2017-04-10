using System;
using System.Collections.Generic;

namespace GameEngine
{
    public partial class ConfPool
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

        private static void InitTabConf<T1, T2>() where T2 : AbsTabConf, new()
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

        private static void InitIniConf<T1, T2>() where T2 : AbsIniConf, new()
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

        private static void InitJsonConf<T1, T2>() where T2 : AbsJsonConf, new()
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

        public static T GetTab<T>(string key) where T : BaseTbl
        {
            Type type = typeof(T);

            Dictionary<string, IConfData> tblPool = null;
            if (!m_tabPools.TryGetValue(type, out tblPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return null;
            }

            IConfData tbl = null;
            if (!tblPool.TryGetValue(key, out tbl)) {
                GameLog.Error(string.Format("{0} 表不存在Key {1}", type.Name, key));
                return null;
            }

            return tbl as T;
        }

        public static Dictionary<string, IConfData> GetTabAll<T>() where T : IConfData
        {
            Type type = typeof(T);

            Dictionary<string, IConfData> tblPool = null;
            if (!m_tabPools.TryGetValue(type, out tblPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return null;
            }

            return tblPool;
        }

        public static int GetTabCount<T>() where T : IConfData
        {
            Type type = typeof(T);

            Dictionary<string, IConfData> tblPool = null;
            if (!m_tabPools.TryGetValue(type, out tblPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return -1;
            }

            return tblPool.Count;
        }


        /// <summary>
        /// 是否包含表
        /// </summary>
        /// <typeparam name="T">表</typeparam>
        /// <returns></returns>
        public static bool HasTab<T>() where T : IConfData
        {
            return m_tabPools.ContainsKey(typeof(T));
        }

        #endregion

        #region Ini 接口

        #endregion

        #region Json 接口

        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Release()
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
