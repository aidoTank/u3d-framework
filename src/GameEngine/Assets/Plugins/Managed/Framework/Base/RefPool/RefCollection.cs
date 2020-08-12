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

        public RefCollection(Type refType)
        {
            m_RefQueue = new Queue<IRef>();
            m_RefType = refType;
        }
    }
}
