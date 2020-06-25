using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    /// <summary>
    /// 任务状态。
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// 未开始。
        /// </summary>
        Todo = 0,
        /// <summary>
        /// 执行中。
        /// </summary>
        Doing = 1, 
        /// <summary>
        /// 完成。
        /// </summary>
        Done = 2
    }
}
