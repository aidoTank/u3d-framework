using System;
using System.Collections.Generic;

namespace Framework
{
    public static class RefPool
    {
        private readonly static Dictionary<Type, RefCollection> m_RefCollection;

        static RefPool()
        {
            m_RefCollection = new Dictionary<Type, RefCollection>();
        }

        public static int Count
        {
            get {
                return m_RefCollection.Count;
            }
        }

        public static void ClearAll()
        {
            lock (m_RefCollection) {
                foreach (KeyValuePair<Type, RefCollection> pair in m_RefCollection) {
                    pair.Value.RemoveAll();
                }
                m_RefCollection.Clear();
            }
        }

        public static T Acquire<T>() where T : class, IRef, new()
        {
            return GetRefCollection(typeof(T)).Acquire<T>();
        }

        public static IRef Acquire(Type refType)
        {
            return GetRefCollection(refType).Acquire();
        }

        public static void Add<T>(int count) where T : class, IRef, new()
        {
            GetRefCollection(typeof(T)).Add<T>(count);
        }

        public static void Add(Type refType, int count)
        {
            GetRefCollection(refType).Add(refType, count);
        }

        public static void Remove<T>(int count) where T : class, IRef
        {
            GetRefCollection(typeof(T)).Remove(count);
        }

        public static void Remove(Type refType, int count)
        {
            GetRefCollection(refType).Remove(count);
        }

        public static void RemoveAll<T>() where T : class, IRef
        {
            GetRefCollection(typeof(T)).RemoveAll();
        }

        public static void RemoveAll(Type refType)
        {
            GetRefCollection(refType).RemoveAll();
        }

        private static RefCollection GetRefCollection(Type refType)
        {
            if (refType == null) {
                throw new FrameworkException("RefType is invalid.");
            }

            RefCollection refCollection = null;
            lock (m_RefCollection) {
                if (!m_RefCollection.TryGetValue(refType, out refCollection)) {
                    refCollection = new RefCollection(refType);
                    m_RefCollection.Add(refType, refCollection);
                }
            }
            return refCollection;
        }
    }
}
