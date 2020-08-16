using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class RefCollection
    {
        private readonly Queue<IRef> m_RefQueue;
        private readonly Type m_RefType;

        private int m_UsingRefCount;
        private int m_AcquireRefCount;
        private int m_ReleaseRefCount;
        private int m_AddRefCount;
        private int m_RemoveRefCount;

        public Type RefType
        {
            get {
                return m_RefType;
            }
        }

        public int UsingRefCount
        {
            get {
                return m_UsingRefCount;
            }
        }

        public int AcquireRefCount
        {
            get {
                return m_AcquireRefCount;
            }
        }

        public int ReleaseRefCount
        {
            get {
                return m_ReleaseRefCount;
            }
        }

        public int AddRefCount
        {
            get {
                return m_AddRefCount;
            }
        }

        public int RemoveRefCount
        {
            get {
                return m_RemoveRefCount;
            }
        }

        public RefCollection(Type refType)
        {
            m_RefQueue = new Queue<IRef>();
            m_RefType = refType;

            m_UsingRefCount = 0;
            m_AcquireRefCount = 0;
            m_ReleaseRefCount = 0;
            m_AddRefCount = 0;
            m_RemoveRefCount = 0;
        }

        public T Acquire<T>() where T : class, IRef, new()
        {
            if (typeof(T) != RefType) {
                throw new FrameworkException(string.Format("Type={0} is invalid.", typeof(T)));
            }
            m_UsingRefCount++;
            m_AcquireRefCount++;
            lock (m_RefQueue) {
                if (m_RefQueue.Count > 0) {
                    return (T)m_RefQueue.Dequeue();
                }
            }
            m_AddRefCount++;
            return new T();
        }

        public IRef Acquire()
        {
            lock (m_RefQueue) {
                if (m_RefQueue.Count > 0) {
                    return m_RefQueue.Dequeue();
                }
            }
            m_AddRefCount++;
            return (IRef)Activator.CreateInstance(RefType);
        }

        public void Release(IRef reference)
        {
            reference.Clear();
            lock (m_RefQueue) {
                if (m_RefQueue.Contains(reference)) {
                    throw new FrameworkException("The reference has been released.");
                }
                m_RefQueue.Enqueue(reference);
            }
            m_UsingRefCount--;
            m_ReleaseRefCount++;
        }

        public void Add<T>(int count) where T : class, IRef, new()
        {
            if (typeof(T) != RefType) {
                throw new FrameworkException(string.Format("Type={0} is invalid.", typeof(T)));
            }
            lock (m_RefQueue) {
                m_AddRefCount += count;
                while (count-- > 0) {
                    m_RefQueue.Enqueue(new T());
                }
            }
        }

        public void Add(Type refType, int count)
        {
            lock (m_RefQueue) {
                m_AddRefCount += count;
                while (count-- > 0) {
                    m_RefQueue.Enqueue((IRef)Activator.CreateInstance(RefType));
                }
            }
        }

        public void Remove(int count)
        {
            lock (m_RefQueue) {
                if (count > m_RefQueue.Count) {
                    count = m_RefQueue.Count;
                }
                m_RemoveRefCount += count;
                while (count-- > 0) {
                    m_RefQueue.Dequeue();
                }
            }
        }

        public void RemoveAll()
        {
            lock (m_RefQueue) {
                m_RemoveRefCount += m_RefQueue.Count;
                m_RefQueue.Clear();
            }
        }
    }
}
