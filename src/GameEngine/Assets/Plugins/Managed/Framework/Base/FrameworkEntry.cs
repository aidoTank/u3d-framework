using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class FrameworkEntry
    {
        private static readonly LinkedList<FrameworkModule> s_FrameworkModules = new LinkedList<FrameworkModule>();

        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (FrameworkModule module in s_FrameworkModules) {
                module.Update(elapseSeconds, realElapseSeconds);
            }
        }

        public static T GetModule<T>() where T : class
        {
            Type interfaceType = typeof(T);
            if (!interfaceType.IsInterface) {
                throw new FrameworkException(TextUtility.Format("You must get module by interface, but '{0}' is not.", interfaceType.FullName));
            }

            if (!interfaceType.FullName.StartsWith("Framework.")) {
                throw new FrameworkException(TextUtility.Format("You must get a Game Framework module, but '{0}' is not.", interfaceType.FullName));
            }

            string moduleName = TextUtility.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null) {
                throw new FrameworkException(TextUtility.Format("Can not find Game Framework module type '{0}'.", moduleName));
            }

            return GetModule(moduleType) as T;
        }

        private static FrameworkModule GetModule(Type moduleType)
        {
            foreach (FrameworkModule module in s_FrameworkModules) {
                if (module.GetType() == moduleType) {
                    return module;
                }
            }
            return CreateModule(moduleType);
        }

        private static FrameworkModule CreateModule(Type moduleType)
        {
            FrameworkModule module = (FrameworkModule)Activator.CreateInstance(moduleType);
            if (module == null) {
                throw new FrameworkException(TextUtility.Format("Can not create module '{0}'.", moduleType.FullName));
            }

            LinkedListNode<FrameworkModule> current = s_FrameworkModules.First;
            while (current != null) {
                if (module.Priority > current.Value.Priority) {
                    break;
                }

                current = current.Next;
            }
            if (current != null) {
                s_FrameworkModules.AddBefore(current, module);
            } else {
                s_FrameworkModules.AddLast(module);
            }

            return module;
        }

        public static void Shutdown()
        {
            for (LinkedListNode<FrameworkModule> current = s_FrameworkModules.Last; current != null; current = current.Previous) {
                current.Value.Shutdown();
            }
            s_FrameworkModules.Clear();
        }
    }
}
