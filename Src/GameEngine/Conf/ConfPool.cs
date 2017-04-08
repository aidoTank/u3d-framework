using System;
using System.Collections.Generic;

namespace GameEngine
{
    public partial class ConfPool
    {
        // Tab表数据
        private static Dictionary<Type, Dictionary<string, Iconf>> m_tblPool;
        private static 

        static ConfPool()
        {
            m_tblPool = new Dictionary<Type, Dictionary<string, BaseTbl>>();
        }

        /// <summary>
        /// 初始化表配置
        /// </summary>
        /// <typeparam name="TblClass"></typeparam>
        /// <typeparam name="CfgClass"></typeparam>
        private static void InitCfg<TblClass, CfgClass>() where CfgClass : BaseCfg, new()
        {
            Type type = typeof(TblClass);
            if (m_tblPool.ContainsKey(type)) {
                GameLog.Error(string.Format("{0} 表已加载", type.Name));
                return;
            }

            CfgClass cfg = new CfgClass();
            cfg.Init();
            m_tblPool.Add(type, cfg.Tbl);
            cfg = null;
        }

        /// <summary>
        /// 根据Key获取表行数据对象
        /// </summary>
        /// <typeparam name="T">表</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetTbl<T>(string key) where T : BaseTbl
        {
            Type type = typeof(T);

            Dictionary<string, BaseTbl> tblPool = null;
            if (!m_tblPool.TryGetValue(type, out tblPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return null;
            }

            BaseTbl tbl = null;
            if (!tblPool.TryGetValue(key, out tbl)) {
                GameLog.Error(string.Format("{0} 表不存在Key {1}", type.Name, key));
                return null;
            }

            return tbl as T;
        }

        /// <summary>
        /// 获取表所有数据
        /// </summary>
        /// <typeparam name="T">表</typeparam>
        /// <returns></returns>
        public static Dictionary<string, BaseTbl> GetTblAll<T>() where T : BaseTbl
        {
            Type type = typeof(T);

            Dictionary<string, BaseTbl> tblPool = null;
            if (!m_tblPool.TryGetValue(type, out tblPool)) {
                GameLog.Error(string.Format("{0} 不存在", type.Name));
                return null;
            }

            return tblPool;
        }

        /// <summary>
        /// 获取表数据大小
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetTblCount<T>() where T : BaseTbl
        {
            Type type = typeof(T);

            Dictionary<string, BaseTbl> tblPool = null;
            if (!m_tblPool.TryGetValue(type, out tblPool)) {
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
        public static bool HasTbl<T>() where T : BaseTbl
        {
            return m_tblPool.ContainsKey(typeof(T));
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Release()
        {
            foreach (Dictionary<string, BaseTbl> value in m_tblPool.Values) {
                value.Clear();
            }
            m_tblPool.Clear();
            m_tblPool = null;
        }
    }
}
