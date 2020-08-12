using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class RefPool
    {

        public static int Count
        {
            get {
                return 0;
            }
        }

        public static void ClearAll()
        {

        }

        public static T Acquire<T>() where T : class, IRef, new()
        {
            return null;
        }

        public static IRef Acquire(Type refType)
        {
            return null;
        }

        public static void Add<T>(int count) where T : class, IRef, new()
        {

        }

        public static void Add(Type refType, int count)
        {
            
        }

        public static void Remove<T>(int count) where T : class, IRef
        {

        }

        public static void Remove(Type refType, int count)
        {

        }

        public static void RemoveAll<T>() where T : class, IRef
        {

        }

        public static void RemoveAll(Type refType)
        {

        }
    }
}
