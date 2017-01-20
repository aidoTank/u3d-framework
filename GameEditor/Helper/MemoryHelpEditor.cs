using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    /// <summary>
    /// 内存管理组件使用案例
    /// </summary>
    public class MemoryHelpEditor
    {
        /// <summary>
        /// 计算文件占用的内存
        /// </summary>
        [MenuItem(MenuConfig.TOOLS_MEMORY_NAME)]
        public static void MathFileMemory()
        {
            Object target = Selection.activeObject;
            if(target == null) {
                return;
            }
            Debug.Log("内存占用：" + EditorUtility.FormatBytes(Profiler.GetRuntimeMemorySize(target)));
        }
    }
}
